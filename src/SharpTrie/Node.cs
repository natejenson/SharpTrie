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
			this.Children = new Dictionary<char, Node>();
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
				this.Children.Add(next, newChild);
				newChild.AddSuffix(newSuffix);
			}
		}

		public IEnumerable<string> GetSuffixes(string prefix = "")
		{
			var result = new List<string>();
			if (this.IsComplete)
			{
				result.Add(prefix + this.Character);
			}
			foreach (var kv in this.Children)
			{
				Node childNode = kv.Value;
				result.AddRange(childNode.GetSuffixes(prefix + this.Character));
			}
			return result;
		}
	}
}
