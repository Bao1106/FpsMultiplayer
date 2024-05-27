using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    ///It is an action to clone a GameObject.
    /// </summary>
    [Action("GameObject/Instantiate")]
    [Pada1.BBCore.Help("Clones the object original and returns the clone")]
    public class Instantiate : GOAction
    {

        ///<value>Input Object to be cloned Parameter.</value>
        [InParam("original")]
        [Pada1.BBCore.Help("Object to be cloned")]
        public GameObject original;

        ///<value>Input position for the clone Parameter.</value>
        [InParam("position")]
        [Pada1.BBCore.Help("position for the clone")]
        public Vector3 position;

        ///<value>OutPut instantiated game object Parameter.</value>
        [OutParam("instantiated")]
        [Pada1.BBCore.Help("Returned game object")]
        public GameObject instantiated;


        /// <summary>Initialization Method of Instantiate.</summary>
        /// <remarks>Installed a GameObject in the position and type dice.</remarks>
        public override void OnStart()
        {
            original = GameObject.Instantiate(original,position,original.transform.rotation) as GameObject;
        }

        /// <summary>Method of Update of Instantiate.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
