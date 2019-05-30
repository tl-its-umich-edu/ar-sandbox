I am following the instructions from the [caliper-net example](https://github.com/IMSGlobal/caliper-net#getting-started).

The code examples aren't perfect. I had to change bits of it like removing the `Current Time` attribute and adding an id string to the created media event.

I also added small parts to modify the data like getting a device id and creating the timestamps using the system date and time.

When running from the Unity editor, the event is sent successfully.

But when running from the iPad, it gives this error after the async POST request fails:

```
2019-05-29 09:22:10.974760-0400 EventsWithCaliper[2157:1463828] Task <6AEEA39A-5D24-4C8E-AA60-C684686996EA>.<1> HTTP load failed (error code: -998 [2:0])
2019-05-29 09:22:10.974815-0400 EventsWithCaliper[2157:1463828] Task <6AEEA39A-5D24-4C8E-AA60-C684686996EA>.<1> finished with error - code: -998
2019-05-29 09:22:10.975854-0400 EventsWithCaliper[2157:1463611] Task <6AEEA39A-5D24-4C8E-AA60-C684686996EA>.<1> load failed with error Error Domain=NSURLErrorDomain Code=-1 "unknown error" UserInfo={_kCFStreamErrorCodeKey=0, NSUnderlyingError=0x28049d950 {Error Domain=kCFErrorDomainCFNetwork Code=-998 "(null)" UserInfo={NSErrorPeerAddressKey=<CFData 0x2829d81e0 [0x1dad3b870]>{length = 16, capacity = 16, bytes = 0x100201bb23f134e50000000000000000}, _kCFStreamErrorCodeKey=0, _kCFStreamErrorDomainKey=2}}, _NSURLErrorFailingURLSessionTaskErrorKey=LocalUploadTask <6AEEA39A-5D24-4C8E-AA60-C684686996EA>.<1>, _NSURLErrorRelatedURLSessionTaskErrorKey=(
    "LocalUploadTask <6AEEA39A-5D24-4C8E-AA60-C684686996EA>.<1>"
), NSLocalizedDescription=unknown error, NSErrorFailingURLStringKey=https://cdp.cloud.unity3d.com/v1/events, NSErrorFailingURLKey=https://cdp.cloud.unity3d.com/v1/events, _kCFStreamErrorDomainKey=2} [-1]
2019-05-29 09:22:10.976350-0400 EventsWithCaliper[2157:1463611] [BoringSSL] nw_protocol_boringssl_get_output_frames(1301) [C2.1:2][0x10db0d3b0] get output frames failed, state 8196
2019-05-29 09:22:10.976383-0400 EventsWithCaliper[2157:1463611] [BoringSSL] nw_protocol_boringssl_get_output_frames(1301) [C2.1:2][0x10db0d3b0] get output frames failed, state 8196
2019-05-29 09:22:10.976469-0400 EventsWithCaliper[2157:1463611] TIC Read Status [2:0x0]: 1:57
2019-05-29 09:22:10.976478-0400 EventsWithCaliper[2157:1463611] TIC Read Status [2:0x0]: 1:57
```

I googled the -998 error and found a couple of threads discussing the issue. [1](https://forum.unity.com/threads/no-error-reported-on-ios.670207/) [2](https://www.reddit.com/r/Unity2D/comments/bkvtbz/post_request_with_binary_field_fails_on_ios/) The latter leads me to believe that the caliper library is using a method for creating async POST requests that is incompatible with iOS for some reason.

I decided to look into alternate ways to use async and tried the code from [this article](https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong#The-Fix-is-In). It worked. So a possible solution is to fix or rewrite the library to use the Client object.

There is also an error that appears before the error above, but it only started happening recently when I started trying different solutions so I am not as worried about it.