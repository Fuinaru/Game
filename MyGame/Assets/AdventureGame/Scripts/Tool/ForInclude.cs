using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



    public class ColorChange : MonoBehaviour
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
                if (rds[0].tag != "Bullet")
                 Color2B(rds[0].material, color, speed);


        }

        public bool isChildrenColorB(Transform trans, Color color)
        {

            Renderer[] rds = trans.GetComponentsInChildren<Renderer>();
        //逐一遍历他的子物体中的Renderer
        if (rds[0].tag != "Bullet")
        if (rds[0].material.color != color) return false;
         return true;
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
           trans=trans.GetChild(0);
       
        Renderer rds = trans.GetComponent<Renderer>();
        ColorA2BCir(rds.material, min, max, speed);
        //   Renderer[] rds = trans.GetComponentsInChildren<Renderer>();
        //逐一遍历他的子物体中的Renderer

        //            if (rds[0].tag !="Bullet" )


        //                ColorA2BCir(rds[0].material, min, max, speed);

           }

        public void materialBecomeWhite(Transform trans)
        {

            Renderer[] rds = trans.GetComponentsInChildren<Renderer>();
        //逐一遍历他的子物体中的Renderer

        if (rds[0].tag != "Bullet")
            rds[0].material.color = Color.white;


  
        }
    }
