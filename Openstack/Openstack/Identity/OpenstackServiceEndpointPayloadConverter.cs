﻿// /* ============================================================================
// Copyright 2014 Hewlett Packard
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ============================================================================ */

namespace Openstack.Identity
{
    using System;
    using System.Web;
    using Newtonsoft.Json.Linq;
    using Openstack.Common;

    /// <inheritdoc/>
    internal class OpenstackServiceEndpointPayloadConverter : IOpenstackServiceEndpointPayloadConverter
    {
        /// <inheritdoc/>
        public OpenstackServiceEndpoint Convert(string payload)
        {
            payload.AssertIsNotNullOrEmpty("payload", "A null or empty service endpoint payload cannot be converted.");

            try
            {
                var endpoint = JObject.Parse(payload);
                var publicUri = (string) endpoint["publicURL"];
                var region = (string)endpoint["region"];
                var version = (string)endpoint["versionId"];
                var versionInfo = (string)endpoint["versionInfo"];
                var versionList = (string)endpoint["versionList"];

                return new OpenstackServiceEndpoint(publicUri, region, version, versionInfo, versionList);
            }
            catch (Exception ex)
            {
                throw new HttpParseException(string.Format("Service endpoint payload could not be parsed. Payload: '{0}'", payload), ex);
            }
        }
    }
}
