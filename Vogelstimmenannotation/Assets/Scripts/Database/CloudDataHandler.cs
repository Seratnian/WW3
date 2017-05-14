using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CloudDataHandler<T> {

    bool CloudIsOlder(T saveFile);
    void Save(T saveFile);
    T Load();
}
