using System;
using System.Collections.Generic;

namespace MediatorAndAdapterConsoleApp
{
	class Program
	{
		public static void Main(string[] args)
		{
			User vasya = new User("Вася");
			User tom = new User("Tom");

			Console.WriteLine("Mediator:");
			vasya.SendMessage("меня зовут Вася. я люблю есть помидоры.");  //send to mediator
			tom.SendMessage("hello Vasya. my name is Tom. i love programming in c#");  //send to mediator
			Console.WriteLine("Now with adapter:");

			TranslateUser tom_adapted = new TranslateUser(tom); //adapter

			vasya.SendMessage("меня зовут Вася. я люблю есть помидоры.");
			tom_adapted.SendMessage("hello Vasya. my name is Tom. i love programming in c#");

			Console.ReadKey();
		}
	}
	public interface IUser
	{
		string GetName();
		string GetMessage();
		void SendMessage(string m);
	}
	public class User : IUser
	{
		private string name;
		private string message;
		public User(string name)
		{
			this.name = name;
		}
		public string GetName()
		{
			return name;
		}
		public string GetMessage()
		{
			return message;
		}
		public void SetMessage(string m)
		{
			this.message = m;
		}
		public void SendMessage(string m)
		{
			this.SetMessage(m);
			ChatRoom.showMessage(this);
		}
	}
	public class TranslateUser : IUser   // adapter
	{
		private User user;
		private Dictionary<string, string> vocabulary;

		public TranslateUser(User user)
		{
			this.user = user;
			vocabulary = new Dictionary<string, string> {
				{"i" , "я"},
				{"hello", "привет"},
				{"Vasya", "Вася"},
				{"name", "имя"},
				{"Tom", "Том"},
				{"love", "люблю"},
				{"programming", "программировать"},
				{"my", "мое"},
				{"in", "на"},
				{"is", "есть"},
				{"read", "читать" },
				{"books", "книги" }
			};
		}

		public string GetName()
		{
			return user.GetName();
		}

		public string GetMessage()
		{
			string message = user.GetMessage(); //english message
			List<string> sentences = new List<string>(message.Split('.'));
			List<string> translated_sentences = new List<string>();

			foreach (string sentence in sentences)
			{
				List<string> words = new List<string>(sentence.Split(' '));
				List<string> translated_words = new List<string>();

				foreach (string word in words)
				{
					if (vocabulary.ContainsKey(word))
					{
						translated_words.Add(vocabulary[word]);
					}
					else
					{
						translated_words.Add(word);
					}
				}
				translated_sentences.Add(string.Join(" ", translated_words.ToArray()));
			}

			string translated_message = string.Join(".", translated_sentences.ToArray()) + ".";
			return translated_message;
		}
		public void SendMessage(string m)
		{
			this.user.SetMessage(m);
			ChatRoom.showMessage(this);
		}
	}

	public class ChatRoom
	{ //mediator
		public static void showMessage(IUser user)
		{
			Console.WriteLine(DateTime.Now + " [" + user.GetName() + "] : " + user.GetMessage());
		}
	}
}

