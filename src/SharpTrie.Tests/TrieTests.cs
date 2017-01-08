using System;
using System.Linq;
using System.Collections.Generic;
using SharpTrie;
using Xunit;

namespace SharpTrie.Tests
{
	public class TrieTests
	{
		[Fact]
		public void Add_GivenNullString_ThrowsException()
		{
			var trie = new Trie();
			Assert.Throws<NullReferenceException>(() => trie.Add(null));
		}

		[Fact]
		public void Add_GivenEmptyString_ThrowsException()
		{
			var trie = new Trie();
			Assert.Throws<ArgumentException>(() => trie.Add(""));
		}

		[Fact]
		public void Find_GivenNullString_ThrowsException()
		{
			var trie = new Trie();
			Assert.Throws<NullReferenceException>(() => trie.Find(null));
		}

		[Fact]
		public void Find_GivenEmptyPrefix_ReturnsAllWords()
		{
			var expected = new List<string>() {"trie", "tree", "nate"};
			var trie = new Trie();
			expected.ForEach(word => trie.Add(word));

			var words = trie.Find("");
			Assert.Equal(0, words.Except(expected).Count());
		}

		[Theory]
		[InlineData("tree")]
		[InlineData("nate")]
		[InlineData("jenson")]
		public void Find_GivenMissingPrefix_EmptyList(string prefix)
		{
			var trie = new Trie();
			trie.Add("foo");
			trie.Add("bar");

			var words = trie.Find(prefix);
			Assert.Equal(0, words.Count());
		}

		[Theory]
		[InlineData("s")]
		[InlineData("search")]
		[InlineData("searchtr")]
		public void Find_GivenPresentPrefix_ReturnsMatchingWords(string prefix)
		{
			var expected = new List<string>() { "searchtrie", "searchtree" };
			var trie = new Trie();
			trie.Add(expected[0]);
			trie.Add(expected[1]);
			trie.Add("another");

			var words = trie.Find(prefix);
			Assert.Equal(0, words.Except(expected).Count());
		}

		[Theory]
		[InlineData("full")]
		[InlineData("z")]

		public void Find_GivenFullWord_ReturnsMatchingWord(string expected)
		{
			var trie = new Trie();
			trie.Add(expected);
			trie.Add("another");
			var words = trie.Find(expected);

			Assert.Equal(true, words.Count() == 1);
			Assert.Equal(expected, words.ToList()[0]);
		}
	}
}
