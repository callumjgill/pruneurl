# Changelog

## [0.1.0](https://github.com/callumjgill/pruneurl/compare/v0.0.4...v0.1.0) (2023-07-09)

### Features

- **frontend:** add collapse transition to generated url ([e7bcc17](https://github.com/callumjgill/pruneurl/commit/e7bcc17dbcfd633441cae1763f32719204ce2d39))
- **frontend:** add toast transitions ([22ed83f](https://github.com/callumjgill/pruneurl/commit/22ed83fecc1e85b0bd8ea779c4a94d532f4b0744))

### Code Refactoring

- **frontend:** split form controls into dedicated components ([0450b41](https://github.com/callumjgill/pruneurl/commit/0450b41f73ccdccacf2b64dc8fe6352099d59302))
- **frontend:** consolidate toast types into one component ([cc7cfcf](https://github.com/callumjgill/pruneurl/commit/cc7cfcf0283861d6a10a5c541cff49d5f984f04c))

### [0.1.1](https://github.com/callumjgill/pruneurl/compare/v0.1.0...v0.1.1) (2023-07-09)

### Bug Fixes

- **frontend:** fix spinner size in form buttons ([f190a0f](https://github.com/callumjgill/pruneurl/commit/f190a0fd3bff430ee1cad5932e0450565daa47d2))

### Documentation

- **changelog:** add heading to changelog ([367f7ba](https://github.com/callumjgill/pruneurl/commit/367f7bae584ed968ad6866965f192af5374bc127))

## [0.2.0](https://github.com/callumjgill/pruneurl/compare/v0.1.1...v0.2.0) (2023-07-09)

### Features

- **frontend:** add responsiveness to app ([51065e0](https://github.com/callumjgill/pruneurl/commit/51065e07c9cb40520bceee7ac0d6c56190a42ba8))
- **frontend:** add useBreakpoint hook ([ee90794](https://github.com/callumjgill/pruneurl/commit/ee9079420e98469d89f9a44ab9cf03671f8a5357))
## [0.3.0](https://github.com/callumjgill/pruneurl/compare/v0.2.0...v0.3.0) (2023-07-13)


### Features

* **frontend:** add 'strings' utils ([5ed4835](https://github.com/callumjgill/pruneurl/commit/5ed4835c89de1dd282eb568ebb1340d08648c2e3))
* **frontend:** add API error enviroment variables for development ([722e373](https://github.com/callumjgill/pruneurl/commit/722e373a63d1708a3c21c059fa365402758f7f9a))
* **frontend:** add API interface and DummyAPI implementation ([a7e2a15](https://github.com/callumjgill/pruneurl/commit/a7e2a15b0dbeed28ba4d8c5307008efb7264337e))
* **frontend:** add getAPI function ([194be4a](https://github.com/callumjgill/pruneurl/commit/194be4a17a110344c02ae0de743770c655bb0f4f))
* **frontend:** add isDevelopment utility ([3530678](https://github.com/callumjgill/pruneurl/commit/3530678c505a67b3e4f944256ff68ce7b85a575a))
* **frontend:** modify PruneUrlForm to submit inputs to API and get returned value ([f191eda](https://github.com/callumjgill/pruneurl/commit/f191eda900d01a72a48d7b7fd317b8685e8a2e71))


### Documentation

* **changelog:** update scopes in changelog ([a4c9264](https://github.com/callumjgill/pruneurl/commit/a4c92643cc136da23b0f7089050d5a439ecf9408))


## [0.4.0](https://github.com/callumjgill/pruneurl/compare/v0.3.0...v0.4.0) (2023-07-14)


### Features

* **frontend:** add CopyToClipboardTooltip ([1feea50](https://github.com/callumjgill/pruneurl/commit/1feea50d2bf1eb8fe18feb156bb2a4c56a1705de))
* **frontend:** add waitAsync function to time utils ([714155b](https://github.com/callumjgill/pruneurl/commit/714155bce69213eab387d6fd1a4d9f95900ea74d))


### Code Refactoring

* **frontend:** extend CopyToClipboardButton to use tooltip once text copied ([1957ba6](https://github.com/callumjgill/pruneurl/commit/1957ba64ba35fb1ae12d8c25a892165a11b58fc9))
* **frontend:** modify DummyAPI to use waitAsync method instead of its private method ([c7bf91a](https://github.com/callumjgill/pruneurl/commit/c7bf91aff116ae14cecf44f86b290902555016ad))


## [0.5.0](https://github.com/callumjgill/pruneurl/compare/v0.4.0...v0.5.0) (2023-07-14)


### Features

* **backend:** add initial ASP.NET 7 app project & solution ([6ca60e4](https://github.com/callumjgill/pruneurl/commit/6ca60e4ba9a099a19456135bb41bce57669e90d1))
* **backend:** add test templates & solution folders for each architecture layer ([869c729](https://github.com/callumjgill/pruneurl/commit/869c72998cc083f8b1938c6fc546e267cade9668))


## [0.6.0](https://github.com/callumjgill/pruneurl/compare/v0.5.0...v0.6.0) (2023-07-15)


### Features

* **backend:** add DateTimeProvider implementation of IDateTimeProvider ([2a9e9d3](https://github.com/callumjgill/pruneurl/commit/2a9e9d31916b08028c580a4829ab1df45e3adb95))
* **backend:** add entity project to domain layer ([d3d1640](https://github.com/callumjgill/pruneurl/commit/d3d164099a3329e490861098af87091cbae0b53c))
* **backend:** add IDateTimeProvider interface ([f0b82a7](https://github.com/callumjgill/pruneurl/commit/f0b82a76e57b0b1daf1c2731b595e68df5499884))
* **backend:** add IEntity interface ([ee3b8d4](https://github.com/callumjgill/pruneurl/commit/ee3b8d4ea5d87f2f3933236e16e4f3fac5fe367b))
* **backend:** add IEntityIdProvider interface ([f0ee06f](https://github.com/callumjgill/pruneurl/commit/f0ee06fbca6c195051844f51eb3e6f62a5ef2458))
* **backend:** add implementation project to application layer ([bf68faa](https://github.com/callumjgill/pruneurl/commit/bf68faa89023605cf8eb2c7042fb2e4609e07cdc))
* **backend:** add Interfaces project to application layer ([3ff63b7](https://github.com/callumjgill/pruneurl/commit/3ff63b7c9d2b63995e94e87ef4d31d3210c345be))
* **backend:** add IShortUrlFactory interface ([76261e7](https://github.com/callumjgill/pruneurl/commit/76261e71e64ae8bd1428d02a0253fae726e07319))
* **backend:** add IShortUrlProvider interface ([a4e7152](https://github.com/callumjgill/pruneurl/commit/a4e71524993ba4f430030c94b700b061e704a434))
* **backend:** add ShortUrl entity record ([7128f1f](https://github.com/callumjgill/pruneurl/commit/7128f1f260b9fc8ed471a51d8a7a11d53e7075d7))
* **backend:** add ShortUrlFactory implementation of IShortUrlFactory ([8ed938d](https://github.com/callumjgill/pruneurl/commit/8ed938df95137d84b60fc8b410c646f579daf341))
* **backend:** add TestHelpers project ([f7ecbad](https://github.com/callumjgill/pruneurl/commit/f7ecbad93d8839199582d383b59363f82d8d0bd9))


### Tests

* **backend:** add application implementation test project ([07aac58](https://github.com/callumjgill/pruneurl/commit/07aac58035d5423356cc4537263d128915ed3d4a))
* **backend:** add entity tests project ([9ecb475](https://github.com/callumjgill/pruneurl/commit/9ecb475b631ad6a5ca76b52e594d6c1f50126639))
* **backend:** add EntityTestHelper ([aadcc34](https://github.com/callumjgill/pruneurl/commit/aadcc341a54156ce74cc5f2e42d912476698f929))
* **backend:** add missing Moq dependency to test projects ([e5e6265](https://github.com/callumjgill/pruneurl/commit/e5e6265b478d69bd45713f4331f12985388cc287))
* **backend:** add ShortUrlFactoryUnitTests ([ba46fe3](https://github.com/callumjgill/pruneurl/commit/ba46fe30e58bd72bdf3a99769e8048a9e85884d4))
* **backend:** add ShortUrlUnitTests ([d692358](https://github.com/callumjgill/pruneurl/commit/d692358ef27f23dbd2e02f9411c7356083f81b15))


