using System.Collections.Generic;
using System.Linq;
using Xunit;
using SharpTrie;

namespace SharpTrie.Tests
{
	public class NodeTests
	{
		[Theory]
		[InlineData('a')]
		[InlineData('z')]
		public void Constructor_GivenChar_SetsCharacter(char c)
		{
			var node = new Node(c);
			Assert.Equal(c, node.Character);
		}

		[Fact]
		public void AddSuffix_EmptySuffix_IsComplete()
		{
			var node = new Node('c');
			node.AddSuffix("");
			Assert.Equal(true, node.IsComplete);
		}

		[Fact]
		public void AddSuffix_Suffix_CreatesChildren()
		{
			var node = new Node('b');
			node.AddSuffix("ar");
			Assert.Equal(true, node.Children.ContainsKey('a'));
			var tmp = node.Children['a'];
			Assert.Equal(true, tmp.Children.ContainsKey('r'));
			tmp = tmp.Children['r'];
			Assert.Equal(true, tmp.IsComplete);
		}

		[Fact]
		public void GetSuffixes_NoPrefix_ReturnsWordsStartingAtNode()
		{
			var node = new Node('t');
			node.AddSuffix("rie");
			node.AddSuffix("ree");
			var words = node.GetSuffixes("");
			var expected = new List<string>() {"tree", "trie"};
			Assert.Equal(0, expected.Except(words).Count());
		}

		[Fact]
		public void GetSuffixes_WithPrefix_ReturnsWordsStartingAtNode()
		{
			var node = new Node('t');
			node.AddSuffix("rie");
			node.AddSuffix("ree");
			var words = node.GetSuffixes("search");
			var expected = new List<string>() { "searchtree", "searchtrie" };
			Assert.Equal(0, expected.Except(words).Count());
		}
	}
}
