# Contents

[Project Description](#project-description)

[List of Files Edited](#list-of-files-edited)

---

# Project Description

I am following the instructions from the [caliper-net example](https://github.com/IMSGlobal/caliper-net#getting-started).

The code examples aren't perfect. I had to change bits of it like removing the `Current Time` attribute and adding an id string to the created media event.

I also added small parts to modify the data like getting a device id and creating the timestamps using the system date and time.

When running from the Unity editor, the event is sent successfully.

However, running a build of the project from an iOS device creates an error with the caliper library:

```
NotSupportedException: System.Reflection.Emit.DynamicMethod::.ctor
  at Newtonsoft.Json.Utilities.DynamicReflectionDelegateFactory.CreateDynamicMethod (System.String name, System.Type returnType, System.Type[] parameterTypes, System.Type owner) [0x00000] in <00000000000000000000000000000000>:0 
  at Newtonsoft.Json.Utilities.DynamicReflectionDelegateFactory.CreateDefaultConstructor[T] (System.Type type) [0x00000] in <00000000000000000000000000000000>:0 
  at Newtonsoft.Json.Serialization.DefaultContractResolver.InitializeContract (Newtonsoft.Json.Serialization.JsonContract contract) [0x00000] in <00000000000000000000000000000000>:0 
  at Newtonsoft.Json.Serialization.DefaultContractResolver.CreateObjectContract (System.Type objectType) [0x00000] in <00000000000000000000000000000000>:0 
  at Newtonsoft.Json.Serialization.DefaultContractResolver.CreateContract (System.Type objectType) [0x00000] in <00000000000000000000000000000000>:0 
  at Newtonsoft.Json.Serialization.DefaultContractResolver.ResolveContract (System.Type type) [0x00000] in <00000000000000000000000000000000>:0 
  at Newtonsoft.Json.Serialization.JsonSerializerInternalWriter.Serialize (Newtonsoft.Json.JsonWriter jsonWriter, System.Object value, System.Type objectType) [0x00000] in <00000000000000000000000000000000>:0 
  at Newtonsoft.Json.JsonSerializer.SerializeInternal (Newtonsoft.Json.JsonWriter jsonWriter, System.Object value, System.Type objectType) [0x00000] in <00000000000000000000000000000000>:0 
  at Newtonsoft.Json.JsonConvert.SerializeObjectInternal (System.Object value, System.Type type, Newtonsoft.Json.JsonSerializer jsonSerializer) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Array.Sort[T] (T[] array, System.Int32 index, System.Int32 length, System.Collections.Generic.IComparer`1[T] comparer) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Net.Http.Headers.HttpHeaders.SetValue[T] (System.String name, T value, System.Func`2[T,TResult] toStringConverter) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.Protocol.CaliperClient.SendData[T] (System.Collections.Generic.IEnumerable`1[T] data) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.Events.Media.MediaEvent..ctor (System.String id, ImsGlobal.Caliper.Events.Action action) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Net.Http.Headers.HttpHeaders.SetValue[T] (System.String name, T value, System.Func`2[T,TResult] toStringConverter) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.Protocol.CaliperClient.Send (System.Collections.Generic.IEnumerable`1[T] events) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Func`2[T,TResult].Invoke (T arg) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Linq.Enumerable+WhereSelectEnumerableIterator`2[TSource,TResult].MoveNext () [0x00000] in <00000000000000000000000000000000>:0 
  at System.Threading.Tasks.Task.WhenAll[TResult] (System.Collections.Generic.IEnumerable`1[T] tasks) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.Protocol.CaliperClient.Send (System.Collections.Generic.IEnumerable`1[T] events) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Net.Http.Headers.HttpHeaders.SetValue[T] (System.String name, T value, System.Func`2[T,TResult] toStringConverter) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.CaliperSensor.SendAsync (System.Collections.Generic.IEnumerable`1[T] events) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.Protocol.CaliperClient.Send (System.Collections.Generic.IEnumerable`1[T] events) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Net.Http.Headers.HttpHeaders.SetValue[T] (System.String name, T value, System.Func`2[T,TResult] toStringConverter) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.CaliperSensor.SendAsync (ImsGlobal.Caliper.Events.Event event) [0x00000] in <00000000000000000000000000000000>:0 
  at CaliperEventManager..cctor () [0x00000] in <00000000000000000000000000000000>:0 
  at System.Net.Http.Headers.HttpHeaders.SetValue[T] (System.String name, T value, System.Func`2[T,TResult] toStringConverter) [0x00000] in <00000000000000000000000000000000>:0 
  at CaliperEventManager.TestCaliperEventAsync () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.Events.UnityAction.Invoke () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.Events.UnityEvent.Invoke () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.ExecuteEvents+EventFunction`1[T1].Invoke (T1 handler, UnityEngine.EventSystems.BaseEventData eventData) [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.ExecuteEvents.Execute[T] (UnityEngine.GameObject target, UnityEngine.EventSystems.BaseEventData eventData, UnityEngine.EventSystems.ExecuteEvents+EventFunction`1[T1] functor) [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.StandaloneInputModule.ProcessTouchPress (UnityEngine.EventSystems.PointerEventData pointerEvent, System.Boolean pressed, System.Boolean released) [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.StandaloneInputModule.ProcessTouchEvents () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.StandaloneInputModule.Process () [0x00000] in <00000000000000000000000000000000>:0 
--- End of stack trace from previous location where exception was thrown ---
  at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw () [0x00000] in <00000000000000000000000000000000>:0 
  at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess (System.Threading.Tasks.Task task) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Runtime.CompilerServices.StrongBox`1[T].System.Runtime.CompilerServices.IStrongBox.set_Value (System.Object value) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.Events.Media.MediaEvent..ctor (System.String id, ImsGlobal.Caliper.Events.Action action) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Net.Http.Headers.HttpHeaders.SetValue[T] (System.String name, T value, System.Func`2[T,TResult] toStringConverter) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.Protocol.CaliperClient.Send (System.Collections.Generic.IEnumerable`1[T] events) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Func`2[T,TResult].Invoke (T arg) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Linq.Enumerable+WhereSelectEnumerableIterator`2[TSource,TResult].MoveNext () [0x00000] in <00000000000000000000000000000000>:0 
  at System.Threading.Tasks.Task.WhenAll[TResult] (System.Collections.Generic.IEnumerable`1[T] tasks) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.Protocol.CaliperClient.Send (System.Collections.Generic.IEnumerable`1[T] events) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Net.Http.Headers.HttpHeaders.SetValue[T] (System.String name, T value, System.Func`2[T,TResult] toStringConverter) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.CaliperSensor.SendAsync (System.Collections.Generic.IEnumerable`1[T] events) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.Protocol.CaliperClient.Send (System.Collections.Generic.IEnumerable`1[T] events) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Net.Http.Headers.HttpHeaders.SetValue[T] (System.String name, T value, System.Func`2[T,TResult] toStringConverter) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.CaliperSensor.SendAsync (ImsGlobal.Caliper.Events.Event event) [0x00000] in <00000000000000000000000000000000>:0 
  at CaliperEventManager..cctor () [0x00000] in <00000000000000000000000000000000>:0 
  at System.Net.Http.Headers.HttpHeaders.SetValue[T] (System.String name, T value, System.Func`2[T,TResult] toStringConverter) [0x00000] in <00000000000000000000000000000000>:0 
  at CaliperEventManager.TestCaliperEventAsync () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.Events.UnityAction.Invoke () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.Events.UnityEvent.Invoke () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.ExecuteEvents+EventFunction`1[T1].Invoke (T1 handler, UnityEngine.EventSystems.BaseEventData eventData) [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.ExecuteEvents.Execute[T] (UnityEngine.GameObject target, UnityEngine.EventSystems.BaseEventData eventData, UnityEngine.EventSystems.ExecuteEvents+EventFunction`1[T1] functor) [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.StandaloneInputModule.ProcessTouchPress (UnityEngine.EventSystems.PointerEventData pointerEvent, System.Boolean pressed, System.Boolean released) [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.StandaloneInputModule.ProcessTouchEvents () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.StandaloneInputModule.Process () [0x00000] in <00000000000000000000000000000000>:0 
--- End of stack trace from previous location where exception was thrown ---
  at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw () [0x00000] in <00000000000000000000000000000000>:0 
  at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess (System.Threading.Tasks.Task task) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Runtime.CompilerServices.StrongBox`1[T].System.Runtime.CompilerServices.IStrongBox.set_Value (System.Object value) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.Protocol.CaliperClient.Send (System.Collections.Generic.IEnumerable`1[T] events) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Net.Http.Headers.HttpHeaders.SetValue[T] (System.String name, T value, System.Func`2[T,TResult] toStringConverter) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.CaliperSensor.SendAsync (System.Collections.Generic.IEnumerable`1[T] events) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.Protocol.CaliperClient.Send (System.Collections.Generic.IEnumerable`1[T] events) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Net.Http.Headers.HttpHeaders.SetValue[T] (System.String name, T value, System.Func`2[T,TResult] toStringConverter) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.CaliperSensor.SendAsync (ImsGlobal.Caliper.Events.Event event) [0x00000] in <00000000000000000000000000000000>:0 
  at CaliperEventManager..cctor () [0x00000] in <00000000000000000000000000000000>:0 
  at System.Net.Http.Headers.HttpHeaders.SetValue[T] (System.String name, T value, System.Func`2[T,TResult] toStringConverter) [0x00000] in <00000000000000000000000000000000>:0 
  at CaliperEventManager.TestCaliperEventAsync () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.Events.UnityAction.Invoke () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.Events.UnityEvent.Invoke () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.ExecuteEvents+EventFunction`1[T1].Invoke (T1 handler, UnityEngine.EventSystems.BaseEventData eventData) [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.ExecuteEvents.Execute[T] (UnityEngine.GameObject target, UnityEngine.EventSystems.BaseEventData eventData, UnityEngine.EventSystems.ExecuteEvents+EventFunction`1[T1] functor) [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.StandaloneInputModule.ProcessTouchPress (UnityEngine.EventSystems.PointerEventData pointerEvent, System.Boolean pressed, System.Boolean released) [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.StandaloneInputModule.ProcessTouchEvents () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.StandaloneInputModule.Process () [0x00000] in <00000000000000000000000000000000>:0 
--- End of stack trace from previous location where exception was thrown ---
  at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw () [0x00000] in <00000000000000000000000000000000>:0 
  at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess (System.Threading.Tasks.Task task) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Runtime.CompilerServices.StrongBox`1[T].System.Runtime.CompilerServices.IStrongBox.set_Value (System.Object value) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.Protocol.CaliperClient.Send (System.Collections.Generic.IEnumerable`1[T] events) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Net.Http.Headers.HttpHeaders.SetValue[T] (System.String name, T value, System.Func`2[T,TResult] toStringConverter) [0x00000] in <00000000000000000000000000000000>:0 
  at ImsGlobal.Caliper.CaliperSensor.SendAsync (ImsGlobal.Caliper.Events.Event event) [0x00000] in <00000000000000000000000000000000>:0 
  at CaliperEventManager..cctor () [0x00000] in <00000000000000000000000000000000>:0 
  at System.Net.Http.Headers.HttpHeaders.SetValue[T] (System.String name, T value, System.Func`2[T,TResult] toStringConverter) [0x00000] in <00000000000000000000000000000000>:0 
  at CaliperEventManager.TestCaliperEventAsync () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.Events.UnityAction.Invoke () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.Events.UnityEvent.Invoke () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.ExecuteEvents+EventFunction`1[T1].Invoke (T1 handler, UnityEngine.EventSystems.BaseEventData eventData) [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.ExecuteEvents.Execute[T] (UnityEngine.GameObject target, UnityEngine.EventSystems.BaseEventData eventData, UnityEngine.EventSystems.ExecuteEvents+EventFunction`1[T1] functor) [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.StandaloneInputModule.ProcessTouchPress (UnityEngine.EventSystems.PointerEventData pointerEvent, System.Boolean pressed, System.Boolean released) [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.StandaloneInputModule.ProcessTouchEvents () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.StandaloneInputModule.Process () [0x00000] in <00000000000000000000000000000000>:0 
--- End of stack trace from previous location where exception was thrown ---
  at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw () [0x00000] in <00000000000000000000000000000000>:0 
  at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess (System.Threading.Tasks.Task task) [0x00000] in <00000000000000000000000000000000>:0 
  at System.Runtime.CompilerServices.StrongBox`1[T].System.Runtime.CompilerServices.IStrongBox.set_Value (System.Object value) [0x00000] in <00000000000000000000000000000000>:0 
  at CaliperEventManager..cctor () [0x00000] in <00000000000000000000000000000000>:0 
  at System.Net.Http.Headers.HttpHeaders.SetValue[T] (System.String name, T value, System.Func`2[T,TResult] toStringConverter) [0x00000] in <00000000000000000000000000000000>:0 
  at CaliperEventManager.TestCaliperEventAsync () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.Events.UnityAction.Invoke () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.Events.UnityEvent.Invoke () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.ExecuteEvents+EventFunction`1[T1].Invoke (T1 handler, UnityEngine.EventSystems.BaseEventData eventData) [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.ExecuteEvents.Execute[T] (UnityEngine.GameObject target, UnityEngine.EventSystems.BaseEventData eventData, UnityEngine.EventSystems.ExecuteEvents+EventFunction`1[T1] functor) [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.StandaloneInputModule.ProcessTouchPress (UnityEngine.EventSystems.PointerEventData pointerEvent, System.Boolean pressed, System.Boolean released) [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.StandaloneInputModule.ProcessTouchEvents () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.EventSystems.StandaloneInputModule.Process () [0x00000] in <00000000000000000000000000000000>:0 
--- End of stack trace from previous location where exception was thrown ---
  at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw () [0x00000] in <00000000000000000000000000000000>:0 
  at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw () [0x00000] in <00000000000000000000000000000000>:0 
  at System.Threading.SendOrPostCallback.Invoke (System.Object state) [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.UnitySynchronizationContext.Exec () [0x00000] in <00000000000000000000000000000000>:0 
  at UnityEngine.UnitySynchronizationContext.Exec () [0x00000] in <00000000000000000000000000000000>:0 
UnityEngine.UnitySynchronizationContext:Exec()
UnityEngine.UnitySynchronizationContext:Exec()
 
(Filename: currently not available on il2cpp Line: -1)
```

# List of Files Edited

I created the Assets/Scripts/CaliperEventManager.cs script and the Assets/Scenes/Main.unity scene.

The dll files in the plugins folder was compiled on Windows from the caliper-net library. All other files were either generated by Unity.

