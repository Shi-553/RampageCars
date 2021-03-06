/// <summary>
/// シーンの種類を管理する列挙型
/// <summary>
public enum SceneType
{
    GameTitle = 0,

    GameOver = 1,

    Manager = 2,

    SampleScene = 3,

    Pause = 4,

    GameClear = 5,

    MAP02 = 6,

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
          SceneType.Pause => 4,              
          SceneType.GameClear => 5,              
          SceneType.MAP02 => 6,              
          _ => 0,                                
      };                                         
   }
}
