using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILinkable
{
    void Link(Rigidbody rigidbody);

    void Unlink();
}
