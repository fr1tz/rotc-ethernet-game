#!/usr/bin/perl
use strict;
use warnings;

my @positions = ();
my $state = 0;

while (my $line = <>) {
    if ($state == 0 and $line =~ /new SimGroup\(TextTriggers\)/) {
        $state = 1;
    } elsif ($state == 1 and $line =~ /new Trigger/) {
        $state = 2;
    } elsif ($state == 2 and $line =~ /position = "(-?\d+\.?\d*) (-?\d+\.?\d*) (-?\d+\.?\d*)";/) {
        push(@positions, [$1, $2, $3]);
    } elsif ($state == 2 and $line =~ /};/) { # end of file
        $state = 1;
    } elsif ($state == 1 and $line =~ /};/) {
        print $line;

        print "    new SimGroup(Plateaus) {\n";
        foreach my $pos (@positions) {
            $pos->[2] -= 1.0;
            print
qq~      new InteriorInstance() {
         canSaveDynamicFields = "1";
         position = "$pos->[0] $pos->[1] $pos->[2]";
         rotation = "1 0 0 0";
         scale = "1 1 1";
         interiorFile = "./src/plateau.dif";
         useGLLighting = "0";
         showTerrainInside = "0";
      };\n~;
        }
        print "    };\n";
        $state = 0;
        @positions = ();

        next;
    }

    print $line;
}
