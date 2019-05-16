using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class InputManager {
    /// <summary>
    /// Returns a list of <see cref="KeyCode"/>s in <paramref name="keyCodes"/> where <paramref name="pred"/> returns true.
    /// </summary>
    /// <param name="keyCodes"></param>
    /// <param name="pred"></param>
    /// <returns>A sublist of <see cref="KeyCode"/>s in <paramref name="keyCodes"/> that have passed the <see cref="Predicate{KeyCode}"/> <paramref name="pred"/></returns>
    public static IEnumerable<KeyCode> FilterKeys(IEnumerable<KeyCode> keyCodes, Predicate<KeyCode> pred) {
        return keyCodes.Where(keyCode => pred.Invoke(keyCode));
    }
}