using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
namespace UnityEngine.XR.Interaction.Toolkit
{
    public class MovementSounds : MonoBehaviour
    {

        public new AudioSource audio;

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
                var controller = m_Controllers[i] as XRController;
                if (controller != null &&
                    controller.enableInputActions &&
                    controller.inputDevice.TryGetFeatureValue(feature, out var controllerInput))
                {
                    if (controllerInput.magnitude > 0.2)
                    {
                        if (!audio.isPlaying)
                        {
                            audio.Play();
                        }
                    }
                    else
                    {
                        audio.Stop();
                    }

                }
            }
        }
    }
}
