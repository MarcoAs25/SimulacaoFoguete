using System.Collections;
using UnityEngine;

public class SecondStage : MonoBehaviour
{
    #region SecondStage Info
        public Rigidbody rb;
        public ParticleSystem ps;
        public float x, y;
        public RocketParameters secondStageParams;
    #endregion

    #region Flags and triggers
        public bool start = false, gcheck = false;
        private bool parachute = false, flagOneTime = false, flagOneTime2 = false;
    #endregion

    #region Others
        public float timeElapsed;
        public MeshRenderer mesh;
        public UiChannel uich;
    #endregion

    
    public void startEngine()
    {
        StartCoroutine(WaitAndStart());
    }

    private IEnumerator WaitAndStart()
    {
        yield return new WaitForSeconds(0.5f);
        rb = GetComponent<Rigidbody>();
        start = true;
        ps.Play();
        SoundManager.instance.PlayEngineStage2();
    }
    private void FixedUpdate()
    {
        HInfoSend();
        
        if (start)
        {
            timeElapsed += Time.fixedDeltaTime;
            VelocityInfoSend();

            if (parachute)
            {
                ParachuteDrag();
                if (!gcheck)
                {
                    AngleReset();
                }
                else
                {
                    ParachuteDetach();
                }
            }
            else
            {
                rb.AddForce(transform.forward * secondStageParams.forceStage * secondStageParams.animCurve.Evaluate(timeElapsed), ForceMode.Force);
            }
            VerifyEngineStop();

        }
    }
    public void AngleReset()
    {
        Quaternion q = new Quaternion(0.70711f, 0f, 0f, 0.70711f);

        float dif = Mathf.Abs(90f - transform.rotation.eulerAngles.x);
        dif = dif % 360f;

        if (rb.velocity.y < 0f && dif > 0.01f && rb.drag > 1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, q, -rb.velocity.y / (y));
        }
    }
    public void AngleReset1()
    {
        Quaternion q = new Quaternion(-0.94609f, 0f, 0f, -0.32392f);
        
        float dif = Mathf.Abs(37.8f - transform.rotation.eulerAngles.x);
        dif = dif % 360f;


        if (rb.velocity.y < 0f && dif > 0.01f)
        {
            if (!flagOneTime2)
            {
                flagOneTime2 = true;
                uich.maxH = transform.position.y;
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, q, -rb.velocity.y / (40f * x));
        }
        if (dif <= 0.1f && rb.velocity.y < 0f && transform.position.y < 200f)
        {
            parachute = true;
        }
    }
    public void VelocityInfoSend()
    {
        uich.velSecondStage = rb.velocity.y;
    }
    public void HInfoSend()
    {
        uich.hStage = transform.position.y;
    }
    public void VerifyEngineStop()
    {
        if (secondStageParams.animCurve.Evaluate(timeElapsed) <= 0f)
        {
            ps.Stop();
            SoundManager.instance.StopEngineStage2();
            AngleReset1();
        }
    }
    public void ParachuteDrag()
    {
        if (!flagOneTime)
        {
            flagOneTime = true;
            rb.drag = 2f;
            mesh.enabled = true;
        }
    }
    public void ParachuteDetach()
    {
        rb.drag = 0f;
        mesh.enabled = false;
        start = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (start && collision.gameObject.tag.Equals("Ground"))
            gcheck = true;
    }
}
