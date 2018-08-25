using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ToolF
{


    public class ForInclude : MonoBehaviour
    {

        public void Color2B(Material target, Color color, float speed)
        {
            target.color = Color.Lerp(target.color, color, speed * Time.deltaTime);
        }
        public void Color2B(Transform trans, Color color, float speed)
        {
            Color m_color = trans.GetComponent<Material>().color;
            color = Color.Lerp(m_color, color, speed * Time.deltaTime);
        }
        public void Color2B(Image target, Color color, float speed)
        {
            target.color = Color.Lerp(target.color, color, speed * Time.deltaTime);
        }

        public void ChildrenColor2B(Transform trans, Color color, float speed)
        {

            Renderer[] rds = trans.GetComponentsInChildren<Renderer>();
            //逐一遍历他的子物体中的Renderer
            foreach (Renderer render in rds)
            {
                if (render.tag != "Bullet")
                    //逐一遍历子物体的子材质（renderer中的material）
                    foreach (Material material in render.materials)
                    {

                        Color2B(material, color, speed);
                        //ColorA2BCir(lowHpEffect.lowHpRedImg.material, material.color, Color.white, 20f);

                    }
            }
        }

        public bool isChildrenColorB(Transform trans, Color color)
        {

            Renderer[] rds = trans.GetComponentsInChildren<Renderer>();
            //逐一遍历他的子物体中的Renderer
            foreach (Renderer render in rds)
            {
                if (render.tag != "Bullet")
                    //逐一遍历子物体的子材质（renderer中的material）
                    foreach (Material material in render.materials)
                    {

                        if (material.color == color) return true;
                        //ColorA2BCir(lowHpEffect.lowHpRedImg.material, material.color, Color.white, 20f);

                    }
            }
            return false;
        }


        bool comp = true;
        public void ColorA2BCir(Material target, Color min, Color max, float speed)
        {
        
            if (comp)
            {
                Color2B(target, max, speed);
                if (target.color.a >= max.a - 0.01) comp = false;
            }
            else
            {
                Color2B(target, min, speed);
                if (target.color.a <= min.a + 0.01) comp = true;
            }

        }
        public void ColorA2BCir(Image target, Color min, Color max, float speed)
        {

            if (comp)
            {
                Color2B(target, max, speed);
                if (target.color.a >= max.a - 0.01) comp = false;
            }
            else
            {
                Color2B(target, min, speed);
                if (target.color.a <= min.a + 0.01) comp = true;
            }

        }
        public void flash(Transform trans, Color min, Color max, float speed) {

            Renderer[] rds = trans.GetComponentsInChildren<Renderer>();
            //逐一遍历他的子物体中的Renderer
            foreach (Renderer render in rds)
            {
                if (render.tag !="Bullet" )
                    //逐一遍历子物体的子材质（renderer中的material）
                    foreach (Material material in render.materials)
                {

                    ColorA2BCir(material, min, max, speed);
                    //ColorA2BCir(lowHpEffect.lowHpRedImg.material, material.color, Color.white, 20f);

                }
            }
        }

        public void materialBecomeWhite(Transform trans)
        {

            Renderer[] rds = trans.GetComponentsInChildren<Renderer>();
            //逐一遍历他的子物体中的Renderer
            foreach (Renderer render in rds)
            {
                if (render.tag != "Bullet")
                    //逐一遍历子物体的子材质（renderer中的material）
                    foreach (Material material in render.materials)
                    {

                        material.color = Color.white;
                        //ColorA2BCir(lowHpEffect.lowHpRedImg.material, material.color, Color.white, 20f);

                    }
            }
        }
    }
}