namespace Songhay.Modules.Bolero.Tests.Models

open System.Collections.Generic
open Xunit
open Xunit.Abstractions

open Songhay.Modules.Bolero.Models

type BoleroPrimitivesTests(testOutputHelper: ITestOutputHelper) =

    static member UriFromClaimTestData : seq<obj[]> =
        seq {
            yield
                [|
                    RestApiMetadata (
                        ApiBase "http://localhost:3001",
                        ClaimsSet <| Dictionary<_,_>(dict [
                            ("endpoint-prefix", "api/player/v1")
                            ("route-for-video-yt-playlist", "video/youtube/playlist/{subFolder}/{blobName}|placeholder")
                        ])
                    )
                    "route-for-video-yt-playlist"
                    [| "foo"; "bar" |]
                    "http://localhost:3001/api/player/v1/video/youtube/playlist/foo/bar?code=placeholder"
                |]
            yield
                [|
                    RestApiMetadata (
                        ApiBase "http://localhost:3001/",
                        ClaimsSet <| Dictionary<_,_>(dict [
                            ("endpoint-prefix", "/api/player/v1/")
                            ("route-for-video-yt-playlist", "/video/youtube/playlist/{subFolder}/{blobName}/|placeholder")
                        ])
                    )
                    "route-for-video-yt-playlist"
                    [| "foo"; "bar" |]
                    "http://localhost:3001/api/player/v1/video/youtube/playlist/foo/bar?code=placeholder"
                |]
            yield
                [|
                    RestApiMetadata (
                        ApiBase "http://localhost:3001/",
                        ClaimsSet <| Dictionary<_,_>(dict [
                            ("endpoint-prefix", "/api/player/v1/")
                            ("route-for-video-yt-playlist", "/video/youtube/playlist/{subFolder}/{blobName}/|placeholder")
                        ])
                    )
                    "route-for-video-yt-playlist"
                    [| "foo" |]
                    None
                |]
            yield
                [|
                    RestApiMetadata (
                        ApiBase "http://localhost:3001/",
                        ClaimsSet <| Dictionary<_,_>(dict [
                            ("endpoint-prefix", "/api/player/v1/")
                        ])
                    )
                    "route-for-video-yt-playlist"
                    [| "foo" |]
                    None
                |]
        }

    [<Theory>]
    [<MemberData(nameof BoleroPrimitivesTests.UriFromClaimTestData)>]
    member this.``RestApiMetadata.ToUriFromClaim test``(input: RestApiMetadata, key: string, args: string[], expectedOriginalString: string option) =

        let actual = input.ToUriFromClaim(key, args)

        if expectedOriginalString.IsNone then
            Assert.True(actual.IsNone)
            testOutputHelper.WriteLine $"{nameof None} expected"
        else
            Assert.True(actual.IsSome)
            testOutputHelper.WriteLine $"{nameof actual}: {actual.Value}"

            Assert.Equal(expectedOriginalString.Value, actual.Value.OriginalString)
