# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.2.0] - 2023-09-22
- Updated Tomlyn.dll to 0.16.2
- Removed name hash from Tomlyn.dll to remove errors on Unity 2021.3+

## [1.1.4] - 2022-06-16
- Always attempt to parse auth

## [1.1.3] - 2022-05-05
- Automatically add registry with credentials when detected in clipboard

## [1.1.2] - 2021-12-09
- Parse clipboard to automatically fill in credentials when adding a new registry
- Enable ``always auth`` by default
- Improved edit credentials ux

## [1.1.0] - 2021-10-03
- Needle fork that strips away all publishing-related tools and only keeps credentials/authentication.

## [0.0.1] - 2020-05-11
- Package created