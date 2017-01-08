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
		public void Constructor_GivenChar_NoChildren()
		{
			var node = new Node('a');
			Assert.NotNull(node.Children);
			Assert.Equal(0, node.Children.Count());
		}
	}
}
