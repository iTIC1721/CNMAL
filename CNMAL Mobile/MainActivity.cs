using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System;

namespace CNMAL_Mobile {

	#region Enum
	public enum Exception {
		NonNeedWordsException,
		RunOutOfStackException,
		NonFormCommentException
	}
	#endregion

	[Activity(Label = "CNMAL_Mobile", MainLauncher = true)]
	public class MainActivity : Activity {

		#region Database
		Stack<int> mainStack;
		Dictionary<string, int> dividePoint;
		#endregion

		bool isQuited = false;

		#region Components
		public Button compileButton;
		public Button resetButton;
		public EditText inputText;
		public EditText outputText;
		#endregion

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			mainStack = new Stack<int>();
			dividePoint = new Dictionary<string, int>();

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			compileButton = FindViewById<Button>(Resource.Id.compileButton);
			resetButton = FindViewById<Button>(Resource.Id.resetButton);
			inputText = FindViewById<EditText>(Resource.Id.inputText);
			outputText = FindViewById<EditText>(Resource.Id.outputText);

			compileButton.Click += CompileButton_Click;
			resetButton.Click += ResetButton_Click;
		}

		private void ResetButton_Click(object sender, EventArgs e) {
			inputText.Text = "";
			outputText.Text = "Output\n";
		}

		private void CompileButton_Click(object sender, EventArgs e) {
			if (inputText.Text.Trim() != "")
				Build(inputText.Text.Split('\n'));
		}

		#region Interpret Methods
		/// <summary>
		/// inputBox에 있는 코드를 빌드한다.
		/// </summary>
		/// <param name="inputArray"></param>
		private void Build(string[] inputArray) {
			// 초기화
			isQuited = false;
			outputText.Text = "Output\n";
			mainStack.Clear();

			string[] contentArray = RemoveComment(inputArray);

			if (!SearchNeedWords(contentArray))
				Throw("...", Exception.NonNeedWordsException);


			Interpret(contentArray);
		}

		/// <summary>
		/// 프로그램이 필요한 단어를 갖추고 있는지 확인한다.
		/// </summary>
		/// <param name="textArray"></param>
		/// <returns></returns>
		private bool SearchNeedWords(string[] textArray) {
			bool[] result = new bool[3];
			foreach (string text in textArray) {
				if (text.Contains("자율")) {
					result[0] = true;
					break;
				}
			}
			foreach (string text in textArray) {
				if (text.Contains("창의")) {
					result[1] = true;
					break;
				}
			}
			foreach (string text in textArray) {
				if (text.Contains("품격")) {
					result[2] = true;
					break;
				}
			}
			return result[0] && result[1] && result[2];
		}

		/// <summary>
		/// 문자열에서 주석 부분(소괄호 내부)을 제거한다.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="commentRemovedText"></param>
		/// <returns></returns>
		private string[] RemoveComment(string[] textArray) {
			string[] resultArray = textArray;
			for (int i = 0; i < textArray.Length; i++) {
				int cnsaCount = 0;
				if (isContainCNSANet(textArray[i], out cnsaCount))
					for (int j = 0; j < cnsaCount; j++)
						ConnectCNSANet();

				if (textArray[i].Contains("(") || textArray[i].Contains(")")) {
					if ((textArray[i].Contains("(") && textArray[i].Contains(")")) && (textArray[i].LastIndexOf(')') > textArray[i].LastIndexOf('(') && textArray[i].IndexOf('(') < textArray[i].IndexOf(')'))) {
						string firstString = textArray[i].Substring(0, textArray[i].IndexOf("("));
						string lastString = textArray[i].Substring(textArray[i].LastIndexOf(")") + 1, textArray[i].Length - (textArray[i].LastIndexOf(")") + 1));
						resultArray[i] = firstString + lastString;
					}
					else {
						Throw("...", Exception.NonFormCommentException);
					}
				}
			}
			return resultArray;
		}

		/// <summary>
		/// 문장을 해석해 결과를 출력합니다
		/// </summary>
		/// <param name="textArray"></param>
		private void Interpret(string[] text) {
			for (int i = 0; i < text.Length; i++) {
				int cnsaCount = 0;
				if (isContainCNSANet(text[i], out cnsaCount))
					for (int j = 0; j < cnsaCount; j++)
						ConnectCNSANet();

				if (text[i].Contains("방울토마토")) {
					mainStack.Clear();
				}
				else if (text[i].Contains("큰사넷")) {
					if ((text[i].Contains("빠릅니다") || text[i].Contains("빠른")) && (text[i].Contains("느립니다") || text[i].Contains("느린"))) {
						mainStack.Push(0);
					}
					else if (text[i].Contains("빠릅니다") || text[i].Contains("빠른")) {
						mainStack.Push(1);
					}
					else if (text[i].Contains("느립니다") || text[i].Contains("느린")) {
						mainStack.Push(-1);
					}
					else {
						mainStack.Push(0);
					}
				}
				else {
					if (text[i].StartsWith("자율적인 큰사이언은 ")) {
						mainStack.Push(text[i].Split(' ').Length - 2);
					}
					else if (text[i].StartsWith("창의적인 큰사이언은 ")) {
						mainStack.Push(text[i].Replace(" ", "").Substring(9).Length);
					}
					else if (text[i].StartsWith("품격있는 큰사이언은 ")) {
						int wordCount = text[i].Split(' ').Length - 2;
						if (mainStack.Count >= wordCount) {
							int multi = 1;
							for (int j = 0; j < wordCount; j++) {
								multi *= mainStack.Pop();
							}
							mainStack.Push(multi);
						}
						else {
							Throw("...", Exception.RunOutOfStackException);
						}
					}
					else if (text[i].StartsWith("지금은 ") && text[i].EndsWith("입니다")) {
						if (!dividePoint.ContainsKey(text[i].Replace("지금은 ", "").Replace("입니다", "").Trim()))
							dividePoint.Add(text[i].Replace("지금은 ", "").Replace("입니다", "").Trim(), i);
					}
					else if (text[i].Contains("친구들은 ")) {
						string[] removedText = text[i].Split(new string[] { "친구들이 말하기를 " }, StringSplitOptions.None);
						int[] wordCount = { removedText[0].Trim().Split(' ').Length, removedText[1].Split(' ').Length };
						if (mainStack.Count >= wordCount[0]) {
							int value = GetStackIndexOf(mainStack, wordCount[0]);
							for (int j = 0; j < wordCount[1]; j++) {
								mainStack.Push(value);
							}
						}
						else {
							Throw("...", Exception.RunOutOfStackException);
						}
					}
					else if (text[i].Contains("친구들이 말하기를 ")) {
						string[] removedText = text[i].Split(new string[] { "친구들이 말하기를 " }, StringSplitOptions.None);
						int wordCount = removedText[1].Split(' ').Length;
						if (mainStack.Count >= wordCount) {
							int sum = 0;
							for (int j = 0; j < wordCount; j++) {
								sum += mainStack.Pop();
							}
							Print(sum.ToString());
						}
						else {
							Throw("...", Exception.RunOutOfStackException);
						}
					}
					else if (text[i].Contains("선생님은 ")) {
						string[] removedText = text[i].Split(new string[] { "선생님은 " }, StringSplitOptions.None);
						int wordCount = removedText[1].Split(' ').Length;
						if (mainStack.Count >= wordCount) {
							int[] nums = new int[wordCount];
							for (int j = 0; j < wordCount; j++) {
								nums[i] = -1 * mainStack.Pop();
							}
							for (int j = wordCount - 1; j >= 0; j--) {
								mainStack.Push(nums[i]);
							}
						}
						else {
							Throw("...", Exception.RunOutOfStackException);
						}
					}
					else if (text[i].Contains("선생님께서 말씀하시기를 ")) {
						string[] removedText = text[i].Split(new string[] { "선생님께서 말씀하시기를 " }, StringSplitOptions.None);
						int wordCount = removedText[1].Split(' ').Length;
						if (mainStack.Count >= wordCount) {
							int sum = 0;
							for (int j = 0; j < wordCount; j++) {
								sum += mainStack.Pop();
							}
							Print((char)(sum));
						}
						else {
							Throw("...", Exception.RunOutOfStackException);
						}
					}
					else if (text[i].Contains("에는 ")) {
						string[] removedText = text[i].Split(new string[] { "에는 " }, 2, StringSplitOptions.None);
						if (removedText[1].Trim().Split(' ').Length == mainStack.Peek()) {
							if (dividePoint.ContainsKey(removedText[0].Trim())) {
								i = dividePoint[removedText[0].Trim()];
							}
						}
					}
				}

				if (text[i].Contains("겨레의 하나됨과")) {
					//TODO: 교가 제창
				}

				if (text[i].Contains("로그아웃 되었습니다")) {
					Quit();
					return;
				}
			}
		}

		/// <summary>
		/// 문장에 큰사넷이 포함되어 있는지 확인한다
		/// </summary>
		/// <param name="text">확인할 문장</param>
		/// <param name="count">등장한 개수</param>
		/// <returns></returns>
		private bool isContainCNSANet(string text, out int count) {
			count = 0;
			int temp = GetWordCount(text, "큰사넷", false)
				+ GetWordCount(text, "CNSANet", false)
				+ GetWordCount(text, "CNSA Net", false);

			if (temp == 0)
				return false;

			count = temp;
			return true;
		}

		/// <summary>
		/// 큰사넷과 연결을 시도한다
		/// 사실 아무것도 안한다
		/// </summary>
		private void ConnectCNSANet() {
			for (int i = 0; i < 1000000; i++) {
				string cnsa = "http://10.1.100.32/";
				string home = cnsa + "home";
			}
		}
		#endregion

		#region Output Methods
		/// <summary>
		/// 코드의 실행 결과를 출력한다
		/// </summary>
		/// <param name="text"></param>
		private void Print(char text) {
			if (!isQuited) {
				outputText.Text += text.ToString();
			}
		}

		/// <summary>
		/// 코드의 실행 결과를 출력한다
		/// </summary>
		/// <param name="text"></param>
		private void Print(string text) {
			if (!isQuited) {
				outputText.Text += text;
			}
		}

		/// <summary>
		/// 코드의 실행 결과를 출력한다
		/// </summary>
		/// <param name="text"></param>
		private void Print(string[] textArray) {
			if (!isQuited) {
				string text = string.Join("\r\n", textArray);
				outputText.Text += text;
			}
		}

		/// <summary>
		/// 예외를 출력한다
		/// </summary>
		/// <param name="exceptionText"></param>
		/// <param name="type"></param>
		private void Throw(string exceptionText, Exception type) {
			switch (type) {
				case Exception.NonNeedWordsException:
					Print(exceptionText);
					Print("\r\n\r\n프로그램이 품격을 갖추지 않았습니다.\r\n제거 대상입니다.");
					inputText.Text = "";
					isQuited = true;
					break;

				case Exception.RunOutOfStackException:
					Print(exceptionText);
					isQuited = true;
					break;

				case Exception.NonFormCommentException:
					Print(exceptionText);
					isQuited = true;
					break;
			}
		}

		/// <summary>
		/// 프로그램을 정상적으로 종료한다.
		/// </summary>
		private void Quit() {
			Print("\r\n\r\n프로그램이 정상적으로 종료되었습니다.");
			isQuited = true;
		}
		#endregion

		#region Utility Methods
		private int GetWordCount(string source, string target, bool exact) {
			int count = 0;
			string sourceText = exact ? source : source.ToLower();
			string targetText = exact ? target : target.ToLower();
			for (int i = 0; i < source.Length - target.Length + 1; i++)
				if (source.Substring(i, target.Length).Equals(target))
					count++;
			return count;
		}

		private T GetStackIndexOf<T>(Stack<T> stack, int index) {
			T result = default(T);
			for (int i = 0; i < index; i++) {
				result = stack.Pop();
			}
			return result;
		}
		#endregion
	}
}

