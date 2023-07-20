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


## [0.7.0](https://github.com/callumjgill/pruneurl/compare/v0.6.0...v0.7.0) (2023-07-15)


### Features

* **backend:** add ShortUrlProvider implementation of IShortUrlProvider ([0a9a8f8](https://github.com/callumjgill/pruneurl/commit/0a9a8f8b2faf574b75644044952a04042e576a86))


### Tests

* **backend:** add ShortUrlProviderUnitTests ([c10bc16](https://github.com/callumjgill/pruneurl/commit/c10bc16cabe3e26cbaea5ae592686f9d6d80bc8c))
* **backend:** modify ShortUrlFactoryUnitTests to account for refactored ShortUrlFactory code ([eaeef11](https://github.com/callumjgill/pruneurl/commit/eaeef110a7e4b7bd6f430970cc50d1ec397f124d))


### Code Refactoring

* **backend:** extend IShortUrlFactory.Create method with a int? parameter for sequence id ([16db624](https://github.com/callumjgill/pruneurl/commit/16db624788fc3138096d72652ac524b07d7e9a2c))
* **backend:** modify ShortUrlFactory to implement IShortUrlFactory & IShortUrlProvider changes ([88dec1a](https://github.com/callumjgill/pruneurl/commit/88dec1a4b4b60688178769d14624ed405f166b98))
* **backend:** replace string parameter in IShortUrlProvider.GetShortUrl with int parameter ([7911a1a](https://github.com/callumjgill/pruneurl/commit/7911a1ae16af3179522ab4ba83040e84187d3687))


## [0.8.0](https://github.com/callumjgill/pruneurl/compare/v0.7.0...v0.8.0) (2023-07-16)


### Features

* **backend:** add 'CreateSequenceId' method to EntityTestHelper ([f000b08](https://github.com/callumjgill/pruneurl/commit/f000b085b24abe9a9d810ac55596ead3933e9e3b))
* **backend:** add ISequenceIdFactory interface to application layer ([07d5edc](https://github.com/callumjgill/pruneurl/commit/07d5edce32479755b41c98f224a9437a5d44c2fb))
* **backend:** add SequenceId entity record ([e3891aa](https://github.com/callumjgill/pruneurl/commit/e3891aa33b85fc191692d0fd46e3fd89911da84a))
* **backend:** add SequenceIdFactory implementation to Application layer ([9ffbe7f](https://github.com/callumjgill/pruneurl/commit/9ffbe7f907e78e12d04776a83ccb7993e2b6fe99))


### Tests

* **backend:** add SequenceIdFactoryUnitTests ([6d36b76](https://github.com/callumjgill/pruneurl/commit/6d36b764c98db3a7734eb8f99349d28a5f64d490))
* **backend:** add SequenceIdUnitTests ([b3975b8](https://github.com/callumjgill/pruneurl/commit/b3975b8507bfcf1578b1d613083e796382fae6c1))


## [0.9.0](https://github.com/callumjgill/pruneurl/compare/v0.8.0...v0.9.0) (2023-07-20)


### Features

* **backend:** add adapters for IDbQuery and IDbTransaction for DTO types to core types ([2f6011c](https://github.com/callumjgill/pruneurl/commit/2f6011cecbd4e3ae1203d2d0aeadb4155874e4a2))
* **backend:** add CollectionReferenceHelper to firestore infrastructure project ([8e87564](https://github.com/callumjgill/pruneurl/commit/8e87564ba21d9a5901157288dec820522f915dbd))
* **backend:** add database interface project to application layer ([e5d4a6a](https://github.com/callumjgill/pruneurl/commit/e5d4a6a1eb26177bd72fa55fe23a684cc9e8bc9b))
* **backend:** add Database project to Infrastructure layer ([3271c2b](https://github.com/callumjgill/pruneurl/commit/3271c2b89a46d5a42e98fdaf7557e4ec6dd54da4))
* **backend:** add DTOs specific to Firestore DB for entities ([f7af7cb](https://github.com/callumjgill/pruneurl/commit/f7af7cb9ea80ff78f960b78486494678d651a666))
* **backend:** add FirebaseDbTransactionFactory implementation of IDbTransactionFactory interface ([568eedc](https://github.com/callumjgill/pruneurl/commit/568eedc493ebb5fe00b3376deccb5dacf74eb503))
* **backend:** add FirestoreDbQuery implementation of IDbQuery ([c10ef57](https://github.com/callumjgill/pruneurl/commit/c10ef5747d9c3454f71dbf992a2c28141a0a1312))
* **backend:** add FirestoreDbQueryFactory implementation of IDbQueryFactory ([d784f6e](https://github.com/callumjgill/pruneurl/commit/d784f6e6163b686fc941f8bef8ca8d4c1e97b4f7))
* **backend:** add FirestoreDbTransaction implementation of IDbTransaction ([09961eb](https://github.com/callumjgill/pruneurl/commit/09961eb15ca0219cb6022267a6dacbd95fb88379))
* **backend:** add generic TestCaseAttribute class to TestHelpers ([4732024](https://github.com/callumjgill/pruneurl/commit/4732024bec01458a15f7f65e09b90849f763bc94))
* **backend:** add IDbQuery interface ([24ec40f](https://github.com/callumjgill/pruneurl/commit/24ec40f61b218da74c278622642eb8c620b2e018))
* **backend:** add IDbQueryFactory interface ([63341e7](https://github.com/callumjgill/pruneurl/commit/63341e77d6bc6af69baa190889136483130ca23a))
* **backend:** add IDbTransaction interface ([5d6e2e3](https://github.com/callumjgill/pruneurl/commit/5d6e2e34c76d7f7908eaaa3b71266cddb1de02db))
* **backend:** add IDbTransactionFactory interface ([e1f5c1d](https://github.com/callumjgill/pruneurl/commit/e1f5c1d30bde270248ee3dae92b4ed082c4c8f58))


### Tests

* **backend:** add FirestoreDbQueryUnitTests ([5dc8347](https://github.com/callumjgill/pruneurl/commit/5dc83473fd5bfa4e2bef749750302648b5285da3))
* **backend:** add Infrastructure.Database test project ([2640659](https://github.com/callumjgill/pruneurl/commit/2640659914548f0a96f548908277b9bd3d0526fc))
* **backend:** add integration tests for FirestoreEntityDTOs ([578c1c7](https://github.com/callumjgill/pruneurl/commit/578c1c750fc4eee976bb4dee27c498602d853ec8))
* **backend:** add StubFirestoreEntity for testing firestore DB code ([0495667](https://github.com/callumjgill/pruneurl/commit/04956670f78ad95437448304801aec9a3ec399e7))
* **backend:** add tests for FirestoreDbTransaction ([0dc1606](https://github.com/callumjgill/pruneurl/commit/0dc1606968bfe80be97430788274887ce6c4fdc6))
* **backend:** add unit tests for CollectionReferenceHelper ([cea7b58](https://github.com/callumjgill/pruneurl/commit/cea7b58d2daf5410a606568d0658808dc8376727))
* **backend:** add unit tests for FirestoreDbQueryAdapter ([9d7a5c5](https://github.com/callumjgill/pruneurl/commit/9d7a5c51cb4276e73d1d5d9a1dd3ed8f77c45b6b))
* **backend:** add unit tests for FirestoreDbQueryFactory ([a29731c](https://github.com/callumjgill/pruneurl/commit/a29731c8848d5defeda67496a9aa9157f1d64fe3))
* **backend:** add unit tests for FirestoreDbTransactionAdapter ([cc32491](https://github.com/callumjgill/pruneurl/commit/cc324917c6f7908703cb07d6acbf8c06961e812c))
* **backend:** add unit tests for FirestoreDbTransactionFactory ([f4cf4bc](https://github.com/callumjgill/pruneurl/commit/f4cf4bca01c96a791d2be058332354252bb17ff4))
* **backend:** add unit tests for FirestoreEntityDTO's ([1e6a37d](https://github.com/callumjgill/pruneurl/commit/1e6a37da3cc77c12dd4611eee0eb54e344f3c391))
* **backend:** add unit tests for the FirestoreDTOProfile ([5b06108](https://github.com/callumjgill/pruneurl/commit/5b061088a8022cade0bf6379b1976179c971af37))
* **backend:** add unit tests for the InvalidEntityTypeMapException class ([a00eb65](https://github.com/callumjgill/pruneurl/commit/a00eb65808228929b2a382737ef593eca72ce0e7))
* **backend:** add utility functions for working with FirestoreDb in test enviroment ([f15d24a](https://github.com/callumjgill/pruneurl/commit/f15d24a03c059783c85c8e19f36aa051a052602a))


