using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ToolF
{


    public class ForInclude : MonoBehaviour
    {

        public void ColorA2B(Material target, Color color, float speed)
        {
            target.color = Color.Lerp(target.color, color, speed * Time.deltaTime);
        }
        public void ColorA2B(Image target, Color color, float speed)
        {
            target.color = Color.Lerp(target.color, color, speed * Time.deltaTime);
        }

        bool comp = true;
        public void ColorA2BCir(Material target, Color min, Color max, float speed)
        {
        
            if (comp)
            {
                ColorA2B(target, max, speed);
                if (target.color.a >= max.a - 0.01) comp = false;
            }
            else
            {
                ColorA2B(target, min, speed);
                if (target.color.a <= min.a + 0.01) comp = true;
            }

        }
        public void ColorA2BCir(Image target, Color min, Color max, float speed)
        {

            if (comp)
            {
                ColorA2B(target, max, speed);
                if (target.color.a >= max.a - 0.01) comp = false;
            }
            else
            {
                ColorA2B(target, min, speed);
                if (target.color.a <= min.a + 0.01) comp = true;
            }

        }
       
    }
}