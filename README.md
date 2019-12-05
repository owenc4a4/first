first
=====
how to use the git
`a.c`
1-0


2019-01-11

git config --add oh-my-zsh.hide-status 1

plugin 
```
oh-my-zsh
powerline fonts
```


# 踏み台
```
# 接続が数十秒で切れる場合の設定
Host *
AddKeysToAgent yes
UseKeychain yes
IdentityFile ~/.ssh/id_rsa
ForwardAgent yes
ServerAliveInterval 25


#多段SSH & ポートフォワーディング
Host middle
  HostName xxx.xxx.xxx
  StrictHostkeyChecking no
  LocalForward 10022 127.0.0.1:10022
  User togo-ma
  UserKnownHostsFile /dev/null

Host target
  HostName host2
  ProxyCommand ssh -CW %h:%p middle
  StrictHostkeyChecking no
  LocalForward 10022 host2:22
  User togo-ma
  UserKnownHostsFile /dev/null
```


```
// get pecl
$ wget http://pear.php.net/go-pear.phar
$ php go-pear.phar


$ lib
$ brew install libyaml
$ brew install ossp-uuid
$ brew install libmemcached
# tar xvfz xxxxxx.tar.gz
# libuuid https://noknow.info/it/os/install_libuuid_from_source?lang=ja

# brew install zlib
# brew install pkg-config
```
