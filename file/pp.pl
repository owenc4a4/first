use 5.016;
use strict;
use warnings;


use lib 'lib';
use Neo::Models;
use JSON::XS;
use LWP::UserAgent;
use Time::Piece::Plus;
use Data::Dumper;
use Neo::Util qw/uuid_digest/;

my $ua = LWP::UserAgent->new;
my $host = "http://localhost:5099";
my $session_id;
{
    my $token = models("DB::Player")->single({id => 1})->uid;
    my $res = $ua->post($host. '/api/login', [
        token => $token,
    ]);
    #say "yes: ", $res->content;
    $session_id = decode_json($res->content)->{result};
}
sub post {
    my ($uri, $args) = @_;
    my $res = $ua->post($host. $uri, [
        _neo_sid => $session_id,
        connection_id => Neo::Util::uuid_digest(),
        %$args
    ]);
    say $res->code;
    say $res->content;
}

