# Changelog

All notable changes to Buildvana.Cake will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## Unreleased changes

### New features

### Changes to existing features

- When a version specification change is applied by the `Release` task, the old version spec is now logged together with the new one.

### Bugs fixed in this release

### Known problems introduced by this release

## [1.0.16-preview](https://github.com/Tenacom/Buildvana.Cake/releases/tag/1.0.16-preview) (2024-05-10)

### Bugs fixed in this release

- Dependency `Docfx.App` has been downgraded from version 2.76.0 to version 2.75.3, in order to avoid Roslyn version conflicts with Cake.
- Service `ChangelogService` was not registered for Dependency Injection.

## [1.0.11-preview](https://github.com/Tenacom/Buildvana.Cake/releases/tag/1.0.11-preview) (2024-05-10)

### New features

- Work has begun to support CI systems other than GitHub, although GitHub is for now the only one actually supported.

### Changes to existing features

- Scripts have been completely reorganized and massively refactored.
- Tasks `CleanAll` and `LocalCleanAll` have been replaced by `Prepare`.

### Bugs fixed in this release

- `Buildvana.Cake`'s NuGet package now declares no dependencies. Previously it was "dependent" on the target platform for which it was "built", which makes little sense as it contains no compiled code.

## [1.0.4-preview](https://github.com/Tenacom/Buildvana.Cake/releases/tag/1.0.4-preview) (2024-04-20)

Initial release, based on scripts already in use at Tenacom.
