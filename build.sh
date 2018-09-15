#!/bin/sh

BRANCH=origin/master
BUILD_WORKSPACE=build
ARTIFACTS_WORKSPACE=gh-pages
UNITY_PATH=/Applications/Unity/Hub/Editor/2018.2.8f1/Unity.app/Contents/MacOS/Unity

if [ -d ${BUILD_WORKSPACE} ]; then
  cd $BUILD_WORKSPACE
  git checkout -f $BRANCH
else
  git worktree add $BUILD_WORKSPACE $BRANCH
  cd $BUILD_WORKSPACE
fi

if [ ! -d ${ARTIFACTS_WORKSPACE} ]; then
  git worktree add $ARTIFACTS_WORKSPACE
  pushd $ARTIFACTS_WORKSPACE
  git reset --hard `git rev-list --max-parents=0 --abbrev-commit HEAD`
  git update-ref -d HEAD
  git reset
  git clean -df
  popd
fi

$UNITY_PATH -quit -batchmode -projectPath . -executeMethod WebGLBuilder.Build -logFile /dev/stdout

pushd $ARTIFACTS_WORKSPACE
git add -A
git commit -m "Add build"
git push -f origin HEAD
popd

git worktree remove $ARTIFACTS_WORKSPACE
git branch -D gh-pages
