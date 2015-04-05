Requirements: mono 3.1

To build:
run ./build.sh
or
xbuild /p:Configuration=Release fsharprle.sln

To run:
use compress.sh or expand.sh or
mono fsharprle/bin/Release/fsharprle.exe compress <string>
mono fsharprle/bin/Release/fsharprle.exe expand <string>
