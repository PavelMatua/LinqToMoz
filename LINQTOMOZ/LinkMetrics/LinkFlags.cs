/* Copyright 2017 Pavel MATUA

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

	http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
using System;
namespace LINQTOMOZ
{
	public enum LinkFlags
	{
		NoFollow = 1,
		SameSubdomain = 2,
		MetaRefresh = 4,
		SameIPAddress = 8,
		SameCBlock = 16,
		SpamScore = 32,
		ThreeOneRedirect = 64,
		ThreeTwoRedirect = 128,
		NoScript = 256,
		OffScreen = 512,
		MetaNoFollow = 2048,
		SameRootDomain = 4096,
		Img = 8192,
		FeedAutoDiscovery = 16384,
		RelCanonical = 32768
	}
}
