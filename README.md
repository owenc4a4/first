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
// SSH鍵をssh-agentに登録
$ ssh-add /Users/user_name/.ssh/id_rsa

// 上記パス（~/.ssh_id_rsa）の場合、下記でも登録可能
$ ssh-add -K

// 登録されていることを確認する
$ ssh-add -l

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

# Python3
```
==> python
Python has been installed as
  /usr/local/bin/python3

Unversioned symlinks `python`, `python-config`, `pip` etc. pointing to
`python3`, `python3-config`, `pip3` etc., respectively, have been installed into
  /usr/local/opt/python/libexec/bin

You can install Python packages with
  pip3 install <package>
They will install into the site-package directory
  /usr/local/lib/python3.7/site-packages

See: https://docs.brew.sh/Homebrew-and-Python
```
