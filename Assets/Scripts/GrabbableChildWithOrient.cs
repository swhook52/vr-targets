﻿using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using UnityEngine;

public class GrabbableChildWithOrient : BaseGrabbable
{
    private bool _grabbing;

    protected override void StartGrab(BaseGrabber grabber)
    {
        if (!_grabbing)
        {
            _grabbing = true;
            base.StartGrab(grabber);

            transform.SetParent(GrabberPrimary.transform);
            transform.rotation = grabber.GrabHandle.rotation;
            transform.position = grabber.GrabHandle.position;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            _grabbing = false;
            transform.SetParent(null);
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            base.EndGrab();
        }

    }

    protected override void EndGrab()
    {
        // Grabbing is toggle based for this script
    }

    protected override void AttachToGrabber(BaseGrabber grabber)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        if (!activeGrabbers.Contains(grabber))
            activeGrabbers.Add(grabber);
    }

    protected override void DetachFromGrabber(BaseGrabber grabber)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
    }
}
