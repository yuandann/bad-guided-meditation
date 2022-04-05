using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;

public class InflateDeflate : MonoBehaviour
{
    private AlembicStreamPlayer player;
    private Animator animator;
    float speed;

    public string[] path;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<AlembicStreamPlayer>();
        animator = GetComponent<Animator>();
        speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animator.GetBool("isStarted") == false)
                animator.SetBool("isStarted", true);
            else
                Inflate();
        }

        else if (Input.GetKeyUp(KeyCode.Space))
            Deflate();

        //if(player.CurrentTime < player.EndTime || player.CurrentTime > player.StartTime)
            //player.CurrentTime += speed * Time.deltaTime;
    }

    private void Deflate()
    {
        //player.LoadFromFile(path[0]);
        animator.SetBool("isInhale", false);
        animator.SetBool("isExhale", true);
    }

    private void Inflate()
    {
        //player.LoadFromFile(path[1]);
        animator.SetBool("isExhale", false);
        animator.SetBool("isInhale", true);
    }

    public void LoadFile(int fileID)
    {
        player.LoadFromFile(path[fileID]);
    }
}
