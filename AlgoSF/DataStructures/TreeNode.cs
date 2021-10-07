using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSF.DataStructures
{
    public class TreeNode
    {
        public Int64 val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(long val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
}
