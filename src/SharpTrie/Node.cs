using System.Collections.Generic;

namespace SharpTrie
{
	public class Node
	{
		public Dictionary<char, Node> Children { get; }
		public bool IsComplete { get; set; }
		public char Character { get; }

		public Node(char c)
		{
			this.Character = c;
			this.Children = new Dictionary<char, Node>();
		}
	}
}
