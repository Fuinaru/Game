using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAbility : MonoBehaviour {
    bool isTeleport = false;
    public IEnumerator StartTeleport(float n, Transform self)
    {
        if (!isTeleport)
        {
            isTeleport = true;
            Tools.PlayParticletAtPosByName("SmokeEffect", self);
            yield return new WaitForSeconds(0.4f);
            if (self.tag == "Monster") Tools.LookAtOnlyYAxis(self, GameManager.player.transform);
            Vector3 position = self.position;
            position.y = 0;
            self.position = position;
            Vector3 dir = Vector3.zero;

            Ray ray1 = new Ray(self.position + new Vector3(0, 0.4f, 0), n * self.forward);
            RaycastHit HitInfo1;
            bool result1 = Physics.Raycast(ray1, out HitInfo1, Mathf.Abs(n) * 8);
            if (result1)
            {
                Vector3 pos = HitInfo1.point;
                pos.y = 0;
                if (n > 0) dir += pos - self.position - self.forward;
                else dir += pos - self.position + self.forward;
            }
            else dir += n * self.forward * 8;
            Ray ray2 = new Ray(self.position + new Vector3(0, 0.4f, 0), Mathf.Abs(n) * self.right);
            RaycastHit HitInfo2;
            bool result2 = Physics.Raycast(ray2, out HitInfo2, Mathf.Abs(n) * 8);
            if (result2)
            {
                Vector3 pos = HitInfo2.point;
                pos.y = 0;
                dir += pos - self.position - self.right;
            }
            else dir += Mathf.Abs(n) * self.right * 8;

            Ray ray3 = new Ray(self.position + new Vector3(0, 0.4f, 0), -Mathf.Abs(n) * self.right);
            RaycastHit HitInfo3;
            bool result3 = Physics.Raycast(ray3, out HitInfo3, Mathf.Abs(n) * 8);
            if (result3)
            {
                Vector3 pos = HitInfo3.point;
                pos.y = 0;
                dir += pos - self.position + self.right;
            }
            else dir -= Mathf.Abs(n) * self.right * 8;

            Ray ray4 = new Ray(self.position + new Vector3(0, 0.4f, 0), dir);
            RaycastHit HitInfo4;
            bool result4 = Physics.Raycast(ray4, out HitInfo4, Mathf.Abs(n) * 8);
            if (result4)
            {
                Vector3 pos = HitInfo4.point;
                pos.y = 0;
                self.position = pos - dir.normalized;
            }
            else self.position += Mathf.Abs(n) * dir.normalized * 8;

            // Debug.Log(HitInfo.collider.name);



            Tools.PlayParticletAtPosByName("SmokeEffect", self);
            isTeleport = false;
        }
    }
}
