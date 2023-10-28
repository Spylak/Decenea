#!/bin/bash

rm -f ./Packages/Decenea.WebAppShared.*.nupkg &&

dotnet pack ./Decenea.WebAppShared/Decenea.WebAppShared.csproj -c Release -o ./Packages &&

git add ./Packages/*.nupkg