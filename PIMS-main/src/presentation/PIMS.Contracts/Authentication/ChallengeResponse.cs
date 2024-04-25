using PIMS.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PIMS.Contracts.Authentication.Enums;

namespace PIMS.Contracts.Authentication
{
    public class ChallengeResponse:BaseResponse
    {
        public ChallengeResponse(ChallengeResponseType authType)
        {
            AuthType=authType;
        }
        public ChallengeResponseType AuthType { get; private set; }
    }
}
