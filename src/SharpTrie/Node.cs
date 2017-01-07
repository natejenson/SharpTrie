using System.Collections.Generic;

namespace SharpTrie
{
	public class Node
	{
		public Dictionary<char, Node> Children { get; }
		public bool IsComplete { get; private set; }
		public char Character { get; }

		public Node(char c)
		{
			this.Character = c;
		}

		public void AddSuffix(string suffix)
		{
			if (string.IsNullOrEmpty(suffix))
			{
				this.IsComplete = true;
				return;
			}

			char next = suffix[0];
			string newSuffix = suffix.Substring(1);

			Node childWithNext = null;
			if (Children.TryGetValue(next, out childWithNext))
			{
				childWithNext.AddSuffix(newSuffix);
			}
			else
			{
				var newChild = new Node(next);
				newChild.AddSuffix(newSuffix);
			}
		}
	}
}
