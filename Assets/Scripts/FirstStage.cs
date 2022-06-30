using UnityEngine;

public class FirstStage : MonoBehaviour
{
    #region FirstStage Info
        private Rigidbody rb;
        [SerializeField] private RocketParameters firstStageParams;
        [SerializeField] private ParticleSystem ps;
        [SerializeField] private float x = 1f,y = 1f;
    #endregion 

    #region Flags and triggers
        public bool start = false;
        private bool detach = false, detachFlag = false, firstEngineFlag = false, SecondEngineFlag = false;
    #endregion

    #region Others
        private float timeElapsed;
        [SerializeField] private GameObject secondStage;
        [SerializeField] private UiChannel uich;
    #endregion


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HInfoSend();
        if (start)
        {
            VelocityInfoSend();

            timeElapsed += Time.fixedDeltaTime;

            DetachTimeVerify();

            FirstEngneStart();

            if (detach)
            {
                HandleDetach();
            }
            else
            {
                AngleSet();
                rb.AddForce(transform.forward * firstStageParams.forceStage * firstStageParams.animCurve.Evaluate(timeElapsed), ForceMode.Force);
            }
        }
    }

    public void AngleSet()
    {
        Quaternion q1 = new Quaternion(-0.89803f, 0f, 0f, 0.43994f);

        if (rb.velocity.y > 0f && !detach)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, q1, rb.velocity.y / (40f * y));
        }
    }
    public void VelocityInfoSend()
    {
        uich.velfirstStage = rb.velocity.y;
    }
    public void HInfoSend()
    {
        uich.hFstage = transform.position.y;
    }
    public void DetachTimeVerify()
    {
        if (timeElapsed >= 5f)
        {
            detach = true;
        }
    }
    public void FirstEngneStart()
    {
        if (!firstEngineFlag)
        {
            firstEngineFlag = true;
            ps.Play();
            SoundManager.instance.PlayEngineStage1();
        }
    }
    public void HandleDetach()
    {
        if (!SecondEngineFlag)
        {
            SecondEngineFlag = true;
            ps.Stop();
            SoundManager.instance.StopEngineStage1();
        }
        DetachStage();
        AngleReset();
    }
    public void AngleReset()
    {

        Quaternion q = new Quaternion(-0.70711f, 0f, 0f, 0.70711f);
        float dif = Mathf.Abs(-90f - transform.rotation.eulerAngles.x);
        dif = dif % 360f;

        if (rb.velocity.y < 0f && dif > 0.01f)
        {
            
            transform.rotation = Quaternion.Lerp(transform.rotation, q, -rb.velocity.y / (40f*x));
        }
        
        if(dif <= 0.01f && transform.position.y < 100f )
        {
            rb.drag = 3.3f;
            ps.Play();
            SoundManager.instance.PlayEngineStage1();
        }

        if(rb.velocity.magnitude <= 0.1f && transform.position.y < 50f)
        {
            ps.Stop();
            SoundManager.instance.StopEngineStage1();
            start = false;
        }
        
    }
    public void DetachStage()
    {
        if (!detachFlag)
        {
            detachFlag = true;
            secondStage.transform.parent = null;
            Rigidbody rbsecondStage = secondStage.AddComponent<Rigidbody>() as Rigidbody;
            rbsecondStage.AddForce(rb.velocity, ForceMode.VelocityChange);
            rb.drag = 0f;
            secondStage.GetComponent<SecondStage>().startEngine();
        }
    }
}
