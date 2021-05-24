using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
namespace UnityEngine.XR.Interaction.Toolkit
{
    public class MovementSounds : MonoBehaviour
    {

        public AudioSource dirtAudio;
        public AudioSource waterAudio;

        /// <summary>
        /// Sets which input axis to use when reading from controller input.
        /// </summary>
        /// <seealso cref="inputBinding"/>
        public enum InputAxes
        {
            /// <summary>
            /// Use the primary touchpad or joystick on a device.
            /// </summary>
            Primary2DAxis = 0,
            /// <summary>
            /// Use the secondary touchpad or joystick on a device.
            /// </summary>
            Secondary2DAxis = 1,
        }

        [SerializeField]
        [Tooltip("A list of controllers that allow move.  If an XRController is not enabled, or does not have input actions enabled, move will not work.")]
        List<XRBaseController> m_Controllers = new List<XRBaseController>();
        /// <summary>
        /// The XRControllers that allow move.  An XRController must be enabled in order to move.
        /// </summary>
        public List<XRBaseController> controllers
        {
            get => m_Controllers;
            set => m_Controllers = value;
        }

        InputAxes m_InputBinding = InputAxes.Primary2DAxis;
        /// <summary>
        /// The 2D Input Axis on the controller devices that will be used to trigger a move.
        /// </summary>
        public InputAxes inputBinding
        {
            get => m_InputBinding;
            set => m_InputBinding = value;
        }

        /// <summary>
        /// Mapping of <see cref="InputAxes"/> to actual common usage values.
        /// </summary>
        static readonly InputFeatureUsage<Vector2>[] k_Vec2UsageList =
        {
            CommonUsages.primary2DAxis,
            CommonUsages.secondary2DAxis,
        };


        // Update is called once per frame
        void Update()
        {
            // Accumulate all the controller inputs
            var input = Vector2.zero;
            var feature = k_Vec2UsageList[(int)m_InputBinding];
            for (var i = 0; i < m_Controllers.Count; ++i)
            {
                // Get controller and check if controller is nulls
                var controller = m_Controllers[i] as XRController;
                if (controller != null &&
                    controller.enableInputActions &&
                    controller.inputDevice.TryGetFeatureValue(feature, out var controllerInput))
                {
                    // If player is walking
                    if (controllerInput.magnitude > 0.2)
                    {
                        // Shoot down a ray to check ground
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, Vector3.down, out hit))
                        {
                            var floortag = hit.collider.gameObject.tag;
                            print(floortag);

                            // If on water, play water sound
                            if ((floortag == "Water"))
                            {
                                if (!waterAudio.isPlaying)
                                {
                                    print("water is playing");
                                    dirtAudio.Stop();
                                    waterAudio.Play();
                                }
                            }

                            // If on dirt, play dirt sound
                            else if (floortag == "Ground")
                            {
                                if (!dirtAudio.isPlaying)
                                {
                                    print("dirt is playing");
                                    waterAudio.Stop();
                                    dirtAudio.Play();
                                }
                            }
                        }
                    }
                    // Player not walking, stop step sounds
                    else
                    {
                        dirtAudio.Stop();
                        waterAudio.Stop();
                    }
                }
            }
        }
    }
}
