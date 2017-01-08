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

			var currentNode = _rootNode;

			foreach (char currentChar in s)
			{
				Node nextNode = null;
				if (currentNode.Children.TryGetValue(currentChar, out nextNode))
				{
					currentNode = nextNode;
				}
				else
				{
					currentNode.Children.Add(currentChar, new Node(currentChar));
					currentNode = currentNode.Children[currentChar];
				}
			}
			currentNode.IsComplete = true;
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
				return _rootNode.Children.Values.SelectMany(n => PrefixSearch(n, n.Character.ToString()));
			}

			// Find the node in the trie where the prefix ends.
			Node prefixNode = FindEndOfPrefix(prefix);

			// Return an empty list if we didn't find this prefix in the trie.
			return prefixNode == null ? new List<string>() : PrefixSearch(prefixNode, prefix);
		}

		/// <summary>
		/// Search for the node in the trie that represents this prefix.
		/// </summary>
		/// <param name="prefix">The prefix to search for.</param>
		/// <returns>The node representing the last character in the given prefix. Null if it cannot be found.</returns>
		private Node FindEndOfPrefix(string prefix)
		{
			Node currentNode = _rootNode;
			int currentCharIndex = 0;
			while (currentCharIndex < prefix.Length && currentNode != null)
			{
				Node nextNode = null;
				currentNode.Children.TryGetValue(prefix[currentCharIndex], out nextNode);
				currentNode = nextNode;
				currentCharIndex++;
			}
			return currentNode;
		}

		/// <summary>
		/// Search the node's subtree for all complete words.
		/// </summary>
		/// <param name="startNode">The node to start the search at.</param>
		/// <param name="prefix">The prefix from the root to the start node.</param>
		/// <returns></returns>
		private static IEnumerable<string> PrefixSearch(Node startNode, string prefix)
		{
			var results = new List<string>();
			if (startNode.IsComplete)
			{
				results.Add(prefix);
			}
			foreach (var charAndNode in startNode.Children)
			{
				results.AddRange(PrefixSearch(charAndNode.Value, prefix + charAndNode.Key));
			}
			return results;
		}
	}
}
