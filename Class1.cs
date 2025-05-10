using MelonLoader;
using UnityEngine;
using ScheduleOne.GameTime;

[assembly: MelonInfo(typeof(TimeDisplayMod.TimeDisplay), "Always Time Display", "1.0.3", "vcwk")]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace TimeDisplayMod
{
    public class TimeDisplay : MelonMod
    {
        private GUIStyle style;
        private Texture2D backgroundTexture;
        private bool showTime = true;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Time Display mod loaded!");

            style = new GUIStyle
            {
                fontSize = 16,
                normal = { textColor = Color.white },
                alignment = TextAnchor.MiddleCenter,
                padding = new RectOffset(4, 4, 2, 2)
            };

            backgroundTexture = new Texture2D(1, 1);
            backgroundTexture.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.7f)); // Transparent black
            backgroundTexture.Apply();
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                showTime = !showTime;
            }
        }

        public override void OnGUI()
        {
            if (!showTime) return;

            TimeManager tm = TimeManager.Instance;
            if (tm == null) return;

            int time = tm.CurrentTime;
            int hour = time / 100;
            int minute = time % 100;
            EDay day = tm.CurrentDay;

            string timeString = $"Day {day} - {hour:D2}:{minute:D2}";
            Vector2 textSize = style.CalcSize(new GUIContent(timeString));
            Rect boxRect = new Rect(Screen.width - textSize.x - 30, 20, textSize.x + 20, textSize.y + 10);

            GUI.DrawTexture(boxRect, backgroundTexture);
            GUI.Label(boxRect, timeString, style);
        }
    }
}
