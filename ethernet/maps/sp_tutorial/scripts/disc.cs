

datablock TriggerData(ThrowDiscAtMe) {
    tickPeriodMS = 200;
};

function ThrowDiscAtMe::onEnterTrigger(%this, %trigger, %obj) {
    if (%obj.isCAT) {
        %origin = NameToID(%trigger.originObjName);

        %obj.client.play2D(DiscIncomingSound);
        throwDiscAt(%origin, %obj);
    }

    Parent::onEnterTrigger(%this, %trigger, %obj);
}

datablock NortDiscData(PussyBlueSeekerDisc : BlueSeekerDisc) {
    impactDamage         = 20;
};

function throwDiscAt(%origin, %target) {
    %disc = new (NortDisc)() {
        dataBlock         = PussyBlueSeekerDisc;
        teamId            = 2;
        initialVelocity  = "0" SPC getRandom(-100,100) SPC getRandom(1,100);
        initialPosition  = %origin.position;
        sourceObject      = %origin;
        sourceSlot       = 0;
        client            = 0;
    };
    MissionCleanup.add(%disc);

    %disc.setTarget(%target);
    %disc.setTargetingMask($TargetingMask::Disc);

}
