using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
  public  class BERecipeDetail
    {
        public int RecipeDetailId { get; set; }
        public int RecipeId { get; set; }
        public int ProductId { get; set; } 
    }
  public class BERecipeDetailList
  {
      public int RecipeDetailId { get; set; }
      public string RecipeName { get; set; }
      public int RecipeId { get; set; }
      public int ProductId { get; set; }
      public string ProductName { get; set; }
  }
}
