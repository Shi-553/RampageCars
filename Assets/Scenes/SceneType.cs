/// <summary>
/// シーンの種類を管理する列挙型
/// <summary>
public enum SceneType
{
    GameTitle = 0,

    GameOver = 1,

    Manager = 2,

    SampleScene = 3,

}
public static class SceneTypeExtension
{
   public static int GetBuildIndex(this SceneType type)
   {
      return type switch                         
      {                                          
          SceneType.GameTitle => 0,              
          SceneType.GameOver => 1,              
          SceneType.Manager => 2,              
          SceneType.SampleScene => 3,              
          _ => 0,                                
      };                                         
   }
}
