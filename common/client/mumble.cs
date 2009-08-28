//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2009, mEthLab Interactive
//------------------------------------------------------------------------------

function initMumbleLink()
{
    $mumbleLinkStatus = "";
    schedule(0, 0, mumbleLinkCheck);
}

function mumbleLinkCheck()
{
    %lastStatus = $mumbleLinkStatus;

    if($Pref::Audio::UseMumbleLink)
    {
        if(Mumble_isAttached())
        {
            $mumbleLinkStatus = "Attached";
        }
        else
        {
            if(Mumble_attach())
            {
                $mumbleLinkStatus = "Attached";
            }
            else
            {
                switch(Mumble_lastErrorCode())
                {
                    case 1: $mumbleLinkStatus = "Link not found"; break;
                    case 2: $mumbleLinkStatus = "Unable to open link"; break;
                    case 3: $mumbleLinkStatus = "Unknown link"; break;
                }
            }
        }
    }
    else
    {
        if(Mumble_isAttached())
            Mumble_detach();

        $mumbleLinkStatus = "Not attached";
    }

    if($mumbleLinkStatus !$= %lastStatus)
        onMumbleLinkStatusChange($mumbleLinkStatus);

    schedule(3000, 0, mumbleLinkCheck);
}

function onMumbleLinkStatusChange(%status)
{
    // This is here to prevent console spam.
    // Other mods should override it with something useful.
}

