using CodeInUnity.Core.Utils;

namespace UnityEngine
{
  public struct LogStyle
  {
    public static readonly LogStyle Warning = new LogStyle("Warning", Color.yellow);
    public static readonly LogStyle Success = new LogStyle("Success", Color.green);
    public static readonly LogStyle Info = new LogStyle("Info", Color.cyan);
    public static readonly LogStyle Danger = new LogStyle("Danger", Color.red);

    public static readonly LogStyle Red = new LogStyle(Color.red);
    public static readonly LogStyle Green = new LogStyle(Color.green);
    public static readonly LogStyle Blue = new LogStyle(Color.blue);
    public static readonly LogStyle Yellow = new LogStyle(Color.yellow);
    public static readonly LogStyle Cyan = new LogStyle(Color.cyan);
    public static readonly LogStyle Magenta = new LogStyle(Color.magenta);
    public static readonly LogStyle White = new LogStyle(Color.white);
    public static readonly LogStyle Gray = new LogStyle(Color.gray);
    public static readonly LogStyle Orange = new LogStyle(new Color(1f, 0.5f, 0f));

    public string title;
    public Color? color;

    public LogStyle(string title, Color color)
    {
      this.title = title;
      this.color = color;
    }

    public LogStyle(Color color)
    {
      this.title = null;
      this.color = color;
    }

    public LogStyle(string title)
    {
      this.title = title;
      this.color = null;
    }

    public string Format(object message)
    {
      string text = (message ?? "").ToString();

      if (!string.IsNullOrEmpty(title))
      {
        text = "<b>" + title + "</b>: " + text;
      }

      if (color.HasValue)
      {
        text = "<color=" + ColorUtils.ToRGBHex(color.Value) + ">" + text + "</color>";
      }

      return text;
    }
  }
}
