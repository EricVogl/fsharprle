This month's challenge is to replicate the compress and expand programs described way back in Software Tools in Pascal, to provide run length encoding.

The compress program takes a text file as input and writes what is hopefully a smaller version of the text as output, while expand inverts the operation.

Compression involves replacing runs of four or more of the same character with a three-character code consisting for a tilde, a letter A through Z indicating 1 through 26 repetitions, and the character to be repeated. Runs of longer than 26 characters require muliple encodings. Literal tildes are represented as a run of length 1.

For example, given this string:
ABBB~CDDDDDEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE

is encoded to this string:
ABBB~A~C~ED~ZE~DE

Clarification:
~~ = ~B~
~~~~~~~~~~~~~~~~~~~~~~~~~~~ = ~Z~~A~

============

Requirements: mono 3.1
You can get from http://fsharp.org/use/mac/ or http://fsharp.org/use/linux/

To build:
run ./build.sh
or
xbuild /p:Configuration=Release fsharprle.sln

To run:
use compress.sh or expand.sh or
mono fsharprle/bin/Release/fsharprle.exe compress <string>
mono fsharprle/bin/Release/fsharprle.exe expand <string>
