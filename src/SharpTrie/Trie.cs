using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpTrie
{
	public class Trie
	{
		private readonly Node _rootNode = new Node(default(char));

		/// <summary>
		/// Add a new string to the trie.
		/// </summary>
		/// <param name="s">The string to add to the trie.</param>
		public void Add(string s)
		{
			_rootNode.AddSuffix(s);
		}

		/// <summary>
		/// Get all of the words in the trie that start with the given prefix.
		/// </summary>
		/// <param name="prefix">The prefix to search for.</param>
		/// <returns>A collection of words that start with a common prefix.</returns>
		public IEnumerable<string> Find(string prefix)
		{
			throw new NotImplementedException();
		}
	}
}
