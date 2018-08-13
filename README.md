# UnityGameEvents

_This small set of classes is based on scriptable-object GameEvents created by [roboryantron](https://github.com/roboryantron) ([repo](https://github.com/roboryantron/Unite2017)). If you're unfamiliar with that concept, check out his [Unite2017 YouTube talk](https://www.youtube.com/watch?v=raQ3iHhE_Kk). I've extended his initial work to add support for sending some dynamic data through the GameEvents._

## The Problem

A `GameEvent` is a [scriptable-object](https://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/scriptable-objects) that can be used similarly to the `UnityEvent` object, but allows for greater decoupling of your code.

Classic `UnityEvent` objects bind the trigger and action inside a Unity Object, and so a given game module needs to know about other game modules. To illustrate I'll use examples from a game I'm working on.

There are two game-modules who's actions are related to each other. The **GameResultsUI** and the **PauseRibbon**. When the **GameResultsUI** is shown, I want to make the user interact with only it, so the usually ever-present **PauseRibbon** must hide itself. The **GameResultsUI** calls a `DidShow` `UnityEvent`, that I can use to trigger `Hide()` on the **PauseRibbon**.

![classic-event](/readme-imgs/classic-event.png)

This binding lives within the **GameResultsUI** game-object. Not ideal if you ever want to have the **GameResultsUI** exist in a place where the **PauseRibbon** doesn't. These two things are tightly bound together even when they don't need to be.

## GameEvent

[roboryantron](https://github.com/roboryantron) `GameEvent` creates an abstracted triggerable (and listenable?) object, so that two game-modules need not know of one another. This is very useful for decoupling your game objects.

Now, instead of two modules know about each other. The trigger module just knows about the scriptable-object:

![GameEvent](/readme-imgs/GameEvent.png)

This decouples the modules, since this can always call `Raise` on the GameEvent, and maybe nothing is listening.

**My Changes**

I've extended his initial GameEvent to support passing some dynamic data through the event. Here is what the new scriptable-object looks like in the inspector:

![Game Won](/readme-imgs/GameWon.png)

It has an `EventType`, for dynamic data (in this case an `enum`). And an extended debugging section (only active when the game is playing) for Raising the event manually with any test data you like.

## GameEventListener

I've also modified the `GameEventListener` a bit to allow for dynamic calls there too.

![GameEventListener](/readme-imgs/GameEventListener.png)

In this case `DidWinHandler` will get called with the correct `GameType`.

## Limitations

**GameEvent** is a bit dumb, since calling `Raise` with dynamic data (in the Unity-inspector) requires a matching signature, I've had to implement several variants of the `Raise()` method. It would be better if I could get the Unity-inspector happy with calling a more generalized or dynamic version of `Raise()`.

**GameEventListener**

Methods called by `GameEventListeners` must have the following signature:

```csharp
public void Method(object _object) { }
```

In my case it would be much more useful for the `GameEventListener` to know the `EventType` of the `GameEvent` it's watching, and show an `Event` field that matched that format...

```csharp
public void Method(GameType _gameType) { }
```

However, I've yet to wrestle that into the Unity Inspector.
