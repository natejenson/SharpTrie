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
	}
}
