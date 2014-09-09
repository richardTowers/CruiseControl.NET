VAGRANTFILE_API_VERSION = "2"

Vagrant.configure(VAGRANTFILE_API_VERSION) do |config|

  config.vm.define :ubuntu1404

  config.vm.box = "puphpet/ubuntu1404-x64"

  config.vm.provision "shell", inline: "sudo apt-get install -q -y mono-devel mono-xsp"

  config.vm.network "forwarded_port", guest: 8080, host: 8080

end
