use 5.016;


my @lines = split "\n", `git diff --name-status HEAD~10`;

for (@lines) {
    say "line: ", $_;
    my ($action, $file) = $_ =~ m/^(\w)[\s\S]+\/([^\/]*\.unity3d)/;

    say "$action: $file";
}
