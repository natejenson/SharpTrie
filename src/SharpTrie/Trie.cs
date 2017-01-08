using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpTrie
{
	public class Trie
	{
		private readonly Node _rootNode;

		public Trie()
		{
			_rootNode = new Node(default(char));
		}

		/// <summary>
		/// Add a new string to the trie.
		/// </summary>
		/// <param name="s">The string to add to the trie.</param>
		public void Add(string s)
		{
			if (s == null)
			{
				throw new NullReferenceException("Cannot add a null string to the trie.");
			}

			if (s == "")
			{
				throw new ArgumentException("Cannot add an empty word to the trie.");
			}
			_rootNode.AddSuffix(s);
		}

		/// <summary>
		/// Get all of the words in the trie that start with the given prefix.
		/// </summary>
		/// <param name="prefix">The prefix to search for. An empty string returns all
		/// words in the trie.</param>
		/// <returns>A collection of words that start with a common prefix.</returns>
		public IEnumerable<string> Find(string prefix)
		{
			if (prefix == null)
			{
				throw new NullReferenceException("The supplied prefix cannot be null.");
			}

			// If the prefix is empty, return all of the words in the trie.
			if (prefix == "")
			{
				return _rootNode.Children.Values.SelectMany(n => n.GetSuffixes());
			}

			// Find the node in the trie where the prefix ends.
			Node currentNode = _rootNode;
			int currentCharIndex = 0;
			while (currentCharIndex < prefix.Length && currentNode != null)
			{
				Node nextNode = null;
				currentNode.Children.TryGetValue(prefix[currentCharIndex], out nextNode);
				currentNode = nextNode;
				currentCharIndex++;
			}

			// Return an empty list if we didn't find this prefix in the trie.
			if (currentNode == null)
			{
				return new List<string>();
			}

			return currentNode.GetSuffixes(prefix.Substring(0, prefix.Length - 1));
		}
	}
}
