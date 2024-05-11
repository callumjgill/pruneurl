## [0.19.5](https://github.com/callumjgill/pruneurl/compare/v0.19.4...v0.19.5) (2024-05-11)

### Build Systems

- **deps-dev:** bump the npm-dev-dependencies group across 1 directory with 8 updates ([0e6492c](https://github.com/callumjgill/pruneurl/commit/0e6492ca4e2d96844b9017c1901f4787dfd87d84))

## [0.19.4](https://github.com/callumjgill/pruneurl/compare/v0.19.3...v0.19.4) (2024-05-11)

### Build Systems

- **deps:** bump the npm-dependencies group across 1 directory with 4 updates ([668fb47](https://github.com/callumjgill/pruneurl/commit/668fb4753eff4297d274bb7e443ef92dd4e36502))

## [0.19.3](https://github.com/callumjgill/pruneurl/compare/v0.19.2...v0.19.3) (2024-05-11)

### Build Systems

- **deps:** bump the nuget-dependencies group ([2848553](https://github.com/callumjgill/pruneurl/commit/2848553664912c1161daa51c79adb2800548dff6))

## [0.19.2](https://github.com/callumjgill/pruneurl/compare/v0.19.1...v0.19.2) (2024-05-11)

### Build Systems

- **deps:** bump danielpalme/ReportGenerator-GitHub-Action ([ca6fcba](https://github.com/callumjgill/pruneurl/commit/ca6fcba739f0c940aa24ec3e5390bf847cf0b214))

## [0.19.1](https://github.com/callumjgill/pruneurl/compare/v0.19.0...v0.19.1) (2024-05-11)

### Bug Fixes

- **workflows:** add permissions to test workflow so all PRs have code coverage results ([431e013](https://github.com/callumjgill/pruneurl/commit/431e013ba7df4dc33f9b0d4a629d1fee07c3a886))

# [0.19.0](https://github.com/callumjgill/pruneurl/compare/v0.18.3...v0.19.0) (2024-05-11)

### Features

- **backend:** the REST API will return a problems details object for errors ([4b42969](https://github.com/callumjgill/pruneurl/commit/4b42969589a0b1f9cacbe3de0078de52ecf93710))
- **frontend:** add axios client to communicate with backend ([c29fa1e](https://github.com/callumjgill/pruneurl/commit/c29fa1ef21b91219d51f1d1072d2c2ff46943381))

### Code Refactoring

- **frontend:** replace getApi function with hook ([bee62e6](https://github.com/callumjgill/pruneurl/commit/bee62e67b05117ae8d5c0e550311e00d07e61e19))
- **frontend:** the API middleware now uses DTO responses ([ecc4684](https://github.com/callumjgill/pruneurl/commit/ecc4684eb2a0fd5c87203f723c200874e9652625))

## [0.18.3](https://github.com/callumjgill/pruneurl/compare/v0.18.2...v0.18.3) (2024-04-22)

### Build Systems

- **deps-dev:** bump the npm-dev-dependencies group ([893de7e](https://github.com/callumjgill/pruneurl/commit/893de7ea31e6c83ded255af2679512eb9c6dd241))

## [0.18.2](https://github.com/callumjgill/pruneurl/compare/v0.18.1...v0.18.2) (2024-03-28)

### Styles

- **frontend:** submit button occupies fullwidth on sm screens ([4581067](https://github.com/callumjgill/pruneurl/commit/458106774ac4a60f49fb7b414211bae3b5b09a28))

## [0.18.1](https://github.com/callumjgill/pruneurl/compare/v0.18.0...v0.18.1) (2024-03-28)

### Build Systems

- **deps:** remove react-bootstrap-icons frontend dependency ([af8b2be](https://github.com/callumjgill/pruneurl/commit/af8b2be7df2b84066a7dda1eb219666e19af9e2c))

# [0.18.0](https://github.com/callumjgill/pruneurl/compare/v0.17.2...v0.18.0) (2024-03-28)

### Features

- **backend:** add logging behavior to MediatR request pipeline ([9667eee](https://github.com/callumjgill/pruneurl/commit/9667eee1b32a9018a53645250bce324b81e95cb1))

## [0.17.2](https://github.com/callumjgill/pruneurl/compare/v0.17.1...v0.17.2) (2024-03-27)

### Code Refactoring

- **backend:** replace Autofac with Microsoft.Extensions.DependencyInjection ([398e36c](https://github.com/callumjgill/pruneurl/commit/398e36c44f34dfd8079a5f27abe7f06fd6ca2321)), closes [#87](https://github.com/callumjgill/pruneurl/issues/87)
- **backend:** use MediatR behaviours instead of decorators ([dd1eabf](https://github.com/callumjgill/pruneurl/commit/dd1eabf0602c3fdde5089e39cc7c473c34e24f65)), closes [#93](https://github.com/callumjgill/pruneurl/issues/93)

### Build Systems

- **deps:** remove uncessary NuGet dependencies from backend ([b50cb95](https://github.com/callumjgill/pruneurl/commit/b50cb95cf2f5a5a9bce77e499d5be96b0cde85de))

## [0.17.1](https://github.com/callumjgill/pruneurl/compare/v0.17.0...v0.17.1) (2024-03-27)

### Build Systems

- **deps:** bump the github-actions-dependencies group with 3 updates ([99ff8f1](https://github.com/callumjgill/pruneurl/commit/99ff8f1dc606f5d8034bbeaaf86ef0c19bd70478))

# [0.17.0](https://github.com/callumjgill/pruneurl/compare/v0.16.3...v0.17.0) (2024-03-26)

### Features

- **backend:** remove all Firestore database code ([9096c6a](https://github.com/callumjgill/pruneurl/commit/9096c6aa73cb4d6d5794af434da81c26d855cad0))
- **backend:** use EF Core with PostgreSQL provider instead of custom Interfaces ([9103cc3](https://github.com/callumjgill/pruneurl/commit/9103cc39d03fadfe3b08932fa367786726c7ef72))

### Tests

- **backend:** combine coverage into a single report ([92502f1](https://github.com/callumjgill/pruneurl/commit/92502f10bf4bd4d41593743ea2c1d124f6cc624f))

## [0.16.3](https://github.com/callumjgill/pruneurl/compare/v0.16.2...v0.16.3) (2024-03-25)

### Build Systems

- **deps-dev:** bump the npm-dev-dependencies group ([a5933aa](https://github.com/callumjgill/pruneurl/commit/a5933aa43e5489659d58e974972196e94f9f0044))

## [0.16.2](https://github.com/callumjgill/pruneurl/compare/v0.16.1...v0.16.2) (2024-03-25)

### Bug Fixes

- **workflows:** bump app version workflow will install npm packages in correct directory ([182929b](https://github.com/callumjgill/pruneurl/commit/182929b676bf31564f1467556991d0133b65060b))

### Code Refactoring

- **backend:** remove regions & use primary constructors ([a83eaa6](https://github.com/callumjgill/pruneurl/commit/a83eaa640401041f09b85fda4234671cffc3d4d7))
- **backend:** use flat namespaces for each project ([69f38a2](https://github.com/callumjgill/pruneurl/commit/69f38a277dfd8ae86de9730a90776f442cd7cd23))

### Build Systems

- **backend:** enforce file-scoped namespaces ([caa4b61](https://github.com/callumjgill/pruneurl/commit/caa4b61a0efaead6e51f1f3cf78ae4d9126247cd))
- **Husky:** replace Husky node with Husky.Net and remove root npm packages ([543348e](https://github.com/callumjgill/pruneurl/commit/543348e6c1f27f2f2dbd2f95bfaa410678cf399a))

## [0.16.1](https://github.com/callumjgill/pruneurl/compare/v0.16.0...v0.16.1) (2024-03-20)

### Build Systems

- **deps-dev:** bump the npm-dev-dependencies group ([1ced83c](https://github.com/callumjgill/pruneurl/commit/1ced83c3cffce5685a481227c1c9415c1b43ee46))

# [0.16.0](https://github.com/callumjgill/pruneurl/compare/v0.15.4...v0.16.0) (2024-03-20)

### Features

- **frontend:** app migrated from Create-React-App to Vite ([081aada](https://github.com/callumjgill/pruneurl/commit/081aada565f09a2995ebabd21d6a78dd9c1e1399))

### Bug Fixes

- **workflows:** bump_app_version will now update the frontend package.json instead of repos ([b15ad84](https://github.com/callumjgill/pruneurl/commit/b15ad8491d52d5a2f1b131ffd0ae8111c4ee6efb))

## [0.15.4](https://github.com/callumjgill/pruneurl/compare/v0.15.3...v0.15.4) (2024-03-20)

### Build Systems

- **dependabot:** updates will now occur monthly instead of weekly ([ee06568](https://github.com/callumjgill/pruneurl/commit/ee065683f2f8f4db09c1c3f25ff16c271be4cf93))

## [0.15.3](https://github.com/callumjgill/pruneurl/compare/v0.15.2...v0.15.3) (2024-03-18)

### Build Systems

- **dependabot:** add dependabot.yml ([ec421db](https://github.com/callumjgill/pruneurl/commit/ec421db11dd91a7feb68fd052953f4d91cb7f5c8))

## [0.15.2](https://github.com/callumjgill/pruneurl/compare/v0.15.1...v0.15.2) (2024-03-18)

### Build Systems

- **backend:** update to .NET 8 and use central package versioning ([7b781f0](https://github.com/callumjgill/pruneurl/commit/7b781f03b633826616e38ce55f78200040df77f3))

## [0.15.1](https://github.com/callumjgill/pruneurl/compare/v0.15.0...v0.15.1) (2024-03-18)

### Documentation

- **CHANGELOG:** reformat changelog entries to be in descending version order ([f583984](https://github.com/callumjgill/pruneurl/commit/f5839846a8e822478e4434aa784b9077b5ac7554))

### Build Systems

- **dev-deps:** add conventional commits and commit linting ([2f9a560](https://github.com/callumjgill/pruneurl/commit/2f9a56063ece691276864d8a0386817231d3d07f))
- **dev-deps:** add Husky v9.0.11 ([8d8a995](https://github.com/callumjgill/pruneurl/commit/8d8a995cb653d0a1474dc999d3ddd525ba0e8c9d))
- **workflows:** update bump_app_version.yml to create tag after pipeline commits pushed ([7facd76](https://github.com/callumjgill/pruneurl/commit/7facd763dadc3cecff57bd45f597d335d62fb7ad))
- **workflows:** update React build and test workflow to use node 20.x ([396f59f](https://github.com/callumjgill/pruneurl/commit/396f59ff4640086c23ececde4336af20882bf71d))

# [0.15.0](https://github.com/callumjgill/pruneurl/compare/v0.14.1...v0.15.0) (2023-08-17)

### Features

- **backend:** GetShortUrlQueryValdiator will now have validation error for incorrect short url format ([4dce537](https://github.com/callumjgill/pruneurl/commit/4dce537359fa96e0b25c7cb47a79905e20aee620))
- **backend:** ShortUrlProvider will return -1 when an invalid character is found in the short url ([67f72f8](https://github.com/callumjgill/pruneurl/commit/67f72f811d425d097d223fb9005b4db1d08261d3))

### Tests

- **backend:** add unit tests for ShortUrlProvider asserting invalid characters ([f4354c2](https://github.com/callumjgill/pruneurl/commit/f4354c25565b107ef3a92d8d09f407eaeca37e18))

## [0.14.1](https://github.com/callumjgill/pruneurl/compare/v0.14.0...v0.14.1) (2023-08-11)

### Code Refactoring

- **backend:** remove detailed validation message in favour of simpler one ([d21e90b](https://github.com/callumjgill/pruneurl/commit/d21e90bb2124ef9e9481b99a8a4a8d9ffff390fa))

# [0.14.0](https://github.com/callumjgill/pruneurl/compare/v0.13.1...v0.14.0) (2023-08-11)

### Features

- **backend:** add 'CreateSequenceId' command ([c5838df](https://github.com/callumjgill/pruneurl/commit/c5838df5aea5a574f1a05f4e10c32e733bf11699))
- **backend:** add 'GetSequenceId' query ([484e832](https://github.com/callumjgill/pruneurl/commit/484e83212d5b4cd63633ae38a668a75971831cc7))
- **backend:** add autofac registrations for CreateSequenceIdCommand ([7a17afd](https://github.com/callumjgill/pruneurl/commit/7a17afd198003cc8d319ecfcff1fb470d50dc762))
- **backend:** add autofac registrations for GetSequenceIdQuery ([0b0a0a1](https://github.com/callumjgill/pruneurl/commit/0b0a0a192e7f7df7c32afa65c4a1245746f6e46c))
- **backend:** add console logger, autofac registration, http logging, https redirection and exception handler to the App program ([5e0e132](https://github.com/callumjgill/pruneurl/commit/5e0e132fedcae8992c218df30d7c1fc6d90a2e9f))
- **backend:** add extensions for IHost which ensures the underlying db is setup properly ([2463e44](https://github.com/callumjgill/pruneurl/commit/2463e44fa4d8e1efc4ede7d111dd368853add861))
- **backend:** add OpenAPI and Swagger to project ([95c6fab](https://github.com/callumjgill/pruneurl/commit/95c6fab3cd806273cbd97dcdf874434befd72328))
- **backend:** add REST methods for redirecting a short url and creating a new one ([cbb5dc2](https://github.com/callumjgill/pruneurl/commit/cbb5dc2c29165b302d7fd5a9411f7d5d44338e34))
- **backend:** add REST minimal API route mapping ([53743bb](https://github.com/callumjgill/pruneurl/commit/53743bb1609ad9b2f2cd3e485a9ab2aa94fd6a80))

### Tests

- **backend:** add PruneUrl.Backend.API.Tests project ([e806b3a](https://github.com/callumjgill/pruneurl/commit/e806b3a89520c7d52f02fd4aa90603af2b3af783))
- **backend:** add unit tests for 'CreateSequenceId' command ([232a3f5](https://github.com/callumjgill/pruneurl/commit/232a3f59742acbf11b6d6b24c795954cd431f453))
- **backend:** add unit tests for 'GetSequenceId' query ([f0aca38](https://github.com/callumjgill/pruneurl/commit/f0aca38957c3b40d0ce90914ecb4b4691039589d))
- **backend:** extend integration tests for autofac registrations to include new command and query ([00ae395](https://github.com/callumjgill/pruneurl/commit/00ae39526c4a6520edee5fc0446765f5de3fd46e))

### Code Refactoring

- **backend:** remove IEntityIdProvider and refactor ISequenceIdFactory to have id passed into Create method ([452419a](https://github.com/callumjgill/pruneurl/commit/452419a07b254c406eac36ae6b627f8456109418))

## [0.13.1](https://github.com/callumjgill/pruneurl/compare/v0.13.0...v0.13.1) (2023-08-10)

# [0.13.0](https://github.com/callumjgill/pruneurl/compare/v0.12.0...v0.13.0) (2023-07-26)

### Features

- **backend:** add CommandsModule for autofac registrations ([71a3d56](https://github.com/callumjgill/pruneurl/commit/71a3d560e43a732c9ac3a7c8bb14e4b2a7772adc))
- **backend:** add FirebaseModule for autofac registrations ([4bdaf73](https://github.com/callumjgill/pruneurl/commit/4bdaf73489dc53ac43a6a3e7a235037be02c8554))
- **backend:** add FirestoreDbOptions to Firestore infrastructure project ([d865cf6](https://github.com/callumjgill/pruneurl/commit/d865cf62b1324c9271d50bccc3c8de1f76923d60))
- **backend:** add GuidEntityIdProvider implementation of IEntityIdProvider ([8ff767b](https://github.com/callumjgill/pruneurl/commit/8ff767b7dd08530e017e774a66494f25773397b4))
- **backend:** add ImplementationModule for autofac registrations ([0431ccb](https://github.com/callumjgill/pruneurl/commit/0431ccbef9ff594dff902c7bcacbae07e6ac0996))
- **backend:** add InvalidConfigurationException and extensions for IConfiguration to the Firebase infrastructure project ([9831fda](https://github.com/callumjgill/pruneurl/commit/9831fda22c2f72f1be0b215eaeaae58dec5d8ad9))
- **backend:** add IoC project to infrastructure which will use Autofac for the IoC container ([7366a7f](https://github.com/callumjgill/pruneurl/commit/7366a7f9a257169386921e0d180eab56108af580))
- **backend:** add missing GetShortUrlQueryValidator class ([9c792a9](https://github.com/callumjgill/pruneurl/commit/9c792a9e6b7cd017bcf8cba1b8c70ec6324f2d88))
- **backend:** add QueriesModule for autofac registrations ([b03e644](https://github.com/callumjgill/pruneurl/commit/b03e644fde3d04498b47941db761ea6b186d1636))
- **backend:** add RegisterAllModules extension method for the Autofac ContainerBuilder ([051907d](https://github.com/callumjgill/pruneurl/commit/051907d7338e62f1874da688be7d41249d50e180))
- **backend:** add RequestsModule for autofac registrations ([685be6b](https://github.com/callumjgill/pruneurl/commit/685be6b84b8abfca813b281254daa37e1d11187e))
- **backend:** add TransactionModule for autofac registrations ([471eb55](https://github.com/callumjgill/pruneurl/commit/471eb5513e33c295b270509e4f0690fabc846c94))

### Tests

- **backend:** add GetShortUrlQueryValidator unit tests ([c7f206e](https://github.com/callumjgill/pruneurl/commit/c7f206e48e336fd600b1707a435e0b5f44532ab8))
- **backend:** add integration test for registering all components in IoC container (autofac) ([4ad5a1e](https://github.com/callumjgill/pruneurl/commit/4ad5a1e28b6b8152ef7c28a29e0bea7b3057cf9f))
- **backend:** add unit tests for ConfigurationExtensions ([ebe90c9](https://github.com/callumjgill/pruneurl/commit/ebe90c9cb5687299efa0b12339d39c0542be0421))
- **backend:** add unit tests for FirestoreDbOptions ([0e52d35](https://github.com/callumjgill/pruneurl/commit/0e52d35231251cc52597fd692fbe0b1980e9d482))
- **backend:** add unit tests for GuidEntityIdProvider ([8ea1d4b](https://github.com/callumjgill/pruneurl/commit/8ea1d4b3338fe8d4164393325094214127c12ae3))
- **backend:** add unit tests for InvalidConfigurationException ([e6fb052](https://github.com/callumjgill/pruneurl/commit/e6fb052a0932bb238e7b406d44b70235f4cb2afd))

# [0.12.0](https://github.com/callumjgill/pruneurl/compare/v0.11.0...v0.12.0) (2023-07-25)

### Features

- **backend:** add GetShortUrlQuery and related classes ([1f29dd8](https://github.com/callumjgill/pruneurl/commit/1f29dd8a526aaa6cab4f5aa579aace9b0111c749))
- **backend:** add ISequenceIdProvider & implementation for converting a short url back to its sequence id ([8e1f763](https://github.com/callumjgill/pruneurl/commit/8e1f763545b923baad3ad8074fae1e4c75ca2c3a))

### Tests

- **backend:** add unit tests for the GetShortUrlQuery classes ([f42e902](https://github.com/callumjgill/pruneurl/commit/f42e9026f4311933477ca1f711a78c8c59c69ed7))

### Code Refactoring

- **backend:** remove injecting IEntityIdProvider into ShortUrlFactory and use the sequence id integer value for the ShortUrl entity id ([ba83d05](https://github.com/callumjgill/pruneurl/commit/ba83d0538083edf5ad206c4ff485b78626ada10b))

# [0.11.0](https://github.com/callumjgill/pruneurl/compare/v0.10.0...v0.11.0) (2023-07-25)

### Features

- **backend:** add CreateShortUrlCommand use case ([490ee53](https://github.com/callumjgill/pruneurl/commit/490ee5359de308fe57c1a69312e17eca85045a15))
- **backend:** add CreateShortUrlCommand use case classes ([965c759](https://github.com/callumjgill/pruneurl/commit/965c759203027e3b0b695777674ea9e3fdc824c6))
- **backend:** add MediatR request decorator for validation ([3fc266f](https://github.com/callumjgill/pruneurl/commit/3fc266fd207453a407f0c8e4dcdc8741fb4e60ba))

### Tests

- **backend:** add tests for the requests application layer project ([2788b6b](https://github.com/callumjgill/pruneurl/commit/2788b6b5ba516ec94dbf2f0b209c27f45c84aeef))

### Code Refactoring

- **backend:** remove optional short url being supplied to the IShortUrlFactory interface ([f24603c](https://github.com/callumjgill/pruneurl/commit/f24603c76ec2dade9dcc1ef0ac406d605ab7e5d1))

# [0.10.0](https://github.com/callumjgill/pruneurl/compare/v0.9.1...v0.10.0) (2023-07-23)

### Features

- **backend:** add IDbTransaction interface & Firebase implementations for read-write combinations ([9939038](https://github.com/callumjgill/pruneurl/commit/99390383d6b15d234141dd536c7bde4837968400))
- **backend:** add IDbTransactionProvider and implementation to allow running a single IDbTransaction multiple times before succeeding ([fcab4bd](https://github.com/callumjgill/pruneurl/commit/fcab4bd10cd02079e05e8d95f7df3f386cde6164))
- **backend:** add IDbUpdateOperation interface ([4ebd822](https://github.com/callumjgill/pruneurl/commit/4ebd822af86eb3e124552d5c28cf9691d9385595))
- **backend:** add transaction request use case for retrieving and bumping a sequence id ([c65a614](https://github.com/callumjgill/pruneurl/commit/c65a61446ba20ded89f45e6dce09175ee1565856))

### Tests

- **backend:** add tests for configuration project in application layer ([d0ff6ee](https://github.com/callumjgill/pruneurl/commit/d0ff6eec7941c84e2c0d76a7fb4d0a397db88a12))
- **backend:** add tests for the GetAndBumpSequenceIdRequest transaction classes ([613bbcc](https://github.com/callumjgill/pruneurl/commit/613bbccc431a0c7216a21165eb4591bb91b4ed11))
- **backend:** add unit tests for EntityNotFoundException in application layer ([c01d58b](https://github.com/callumjgill/pruneurl/commit/c01d58ba333d57c10269b5252da1b919cc55bb34))
- **backend:** add unit tests for the Firestore IDbTransactionProvider related implementations ([dca939c](https://github.com/callumjgill/pruneurl/commit/dca939cf7f20d76e0676e922108178ab11cc2595))
- **backend:** add unit tests for the IDbTransaction Firebase implementations ([9379298](https://github.com/callumjgill/pruneurl/commit/9379298a64c009d353f5a32598641f019cc8f072))
- **backend:** refactor tests with emulator to be parallelizable ([6f3a7fb](https://github.com/callumjgill/pruneurl/commit/6f3a7fbbdd78e29b77b05eb42dc5d46d9c2e71ae))

### Code Refactoring

- **backend:** add CreateFromExisting method to ISequenceIdFactory ([8b4c912](https://github.com/callumjgill/pruneurl/commit/8b4c912c3a4162e66f46cb51f59c24b38116280e))
- **backend:** rename IDbQuery to IDbGetByIdOperation to follow LSP ([73aecc7](https://github.com/callumjgill/pruneurl/commit/73aecc75973229fb3370d6fad9060bf63e78194b))
- **backend:** split IDbTransaction into smaller interfaces & rename IDbWriteBatch ([00e1c44](https://github.com/callumjgill/pruneurl/commit/00e1c44b342473da535f3fb31b5329625779ed37))

## [0.9.1](https://github.com/callumjgill/pruneurl/compare/v0.9.0...v0.9.1) (2023-07-20)

### Code Refactoring

- **frontend:** remove short url input from form ([a7908c6](https://github.com/callumjgill/pruneurl/commit/a7908c6d16d979888cb9c5d00b13c2714ec7b5cb))

# [0.9.0](https://github.com/callumjgill/pruneurl/compare/v0.8.0...v0.9.0) (2023-07-20)

### Features

- **backend:** add adapters for IDbQuery and IDbTransaction for DTO types to core types ([2f6011c](https://github.com/callumjgill/pruneurl/commit/2f6011cecbd4e3ae1203d2d0aeadb4155874e4a2))
- **backend:** add CollectionReferenceHelper to firestore infrastructure project ([8e87564](https://github.com/callumjgill/pruneurl/commit/8e87564ba21d9a5901157288dec820522f915dbd))
- **backend:** add database interface project to application layer ([e5d4a6a](https://github.com/callumjgill/pruneurl/commit/e5d4a6a1eb26177bd72fa55fe23a684cc9e8bc9b))
- **backend:** add Database project to Infrastructure layer ([3271c2b](https://github.com/callumjgill/pruneurl/commit/3271c2b89a46d5a42e98fdaf7557e4ec6dd54da4))
- **backend:** add DTOs specific to Firestore DB for entities ([f7af7cb](https://github.com/callumjgill/pruneurl/commit/f7af7cb9ea80ff78f960b78486494678d651a666))
- **backend:** add FirebaseDbTransactionFactory implementation of IDbTransactionFactory interface ([568eedc](https://github.com/callumjgill/pruneurl/commit/568eedc493ebb5fe00b3376deccb5dacf74eb503))
- **backend:** add FirestoreDbQuery implementation of IDbQuery ([c10ef57](https://github.com/callumjgill/pruneurl/commit/c10ef5747d9c3454f71dbf992a2c28141a0a1312))
- **backend:** add FirestoreDbQueryFactory implementation of IDbQueryFactory ([d784f6e](https://github.com/callumjgill/pruneurl/commit/d784f6e6163b686fc941f8bef8ca8d4c1e97b4f7))
- **backend:** add FirestoreDbTransaction implementation of IDbTransaction ([09961eb](https://github.com/callumjgill/pruneurl/commit/09961eb15ca0219cb6022267a6dacbd95fb88379))
- **backend:** add generic TestCaseAttribute class to TestHelpers ([4732024](https://github.com/callumjgill/pruneurl/commit/4732024bec01458a15f7f65e09b90849f763bc94))
- **backend:** add IDbQuery interface ([24ec40f](https://github.com/callumjgill/pruneurl/commit/24ec40f61b218da74c278622642eb8c620b2e018))
- **backend:** add IDbQueryFactory interface ([63341e7](https://github.com/callumjgill/pruneurl/commit/63341e77d6bc6af69baa190889136483130ca23a))
- **backend:** add IDbTransaction interface ([5d6e2e3](https://github.com/callumjgill/pruneurl/commit/5d6e2e34c76d7f7908eaaa3b71266cddb1de02db))
- **backend:** add IDbTransactionFactory interface ([e1f5c1d](https://github.com/callumjgill/pruneurl/commit/e1f5c1d30bde270248ee3dae92b4ed082c4c8f58))

### Tests

- **backend:** add FirestoreDbQueryUnitTests ([5dc8347](https://github.com/callumjgill/pruneurl/commit/5dc83473fd5bfa4e2bef749750302648b5285da3))
- **backend:** add Infrastructure.Database test project ([2640659](https://github.com/callumjgill/pruneurl/commit/2640659914548f0a96f548908277b9bd3d0526fc))
- **backend:** add integration tests for FirestoreEntityDTOs ([578c1c7](https://github.com/callumjgill/pruneurl/commit/578c1c750fc4eee976bb4dee27c498602d853ec8))
- **backend:** add StubFirestoreEntity for testing firestore DB code ([0495667](https://github.com/callumjgill/pruneurl/commit/04956670f78ad95437448304801aec9a3ec399e7))
- **backend:** add tests for FirestoreDbTransaction ([0dc1606](https://github.com/callumjgill/pruneurl/commit/0dc1606968bfe80be97430788274887ce6c4fdc6))
- **backend:** add unit tests for CollectionReferenceHelper ([cea7b58](https://github.com/callumjgill/pruneurl/commit/cea7b58d2daf5410a606568d0658808dc8376727))
- **backend:** add unit tests for FirestoreDbQueryAdapter ([9d7a5c5](https://github.com/callumjgill/pruneurl/commit/9d7a5c51cb4276e73d1d5d9a1dd3ed8f77c45b6b))
- **backend:** add unit tests for FirestoreDbQueryFactory ([a29731c](https://github.com/callumjgill/pruneurl/commit/a29731c8848d5defeda67496a9aa9157f1d64fe3))
- **backend:** add unit tests for FirestoreDbTransactionAdapter ([cc32491](https://github.com/callumjgill/pruneurl/commit/cc324917c6f7908703cb07d6acbf8c06961e812c))
- **backend:** add unit tests for FirestoreDbTransactionFactory ([f4cf4bc](https://github.com/callumjgill/pruneurl/commit/f4cf4bca01c96a791d2be058332354252bb17ff4))
- **backend:** add unit tests for FirestoreEntityDTO's ([1e6a37d](https://github.com/callumjgill/pruneurl/commit/1e6a37da3cc77c12dd4611eee0eb54e344f3c391))
- **backend:** add unit tests for the FirestoreDTOProfile ([5b06108](https://github.com/callumjgill/pruneurl/commit/5b061088a8022cade0bf6379b1976179c971af37))
- **backend:** add unit tests for the InvalidEntityTypeMapException class ([a00eb65](https://github.com/callumjgill/pruneurl/commit/a00eb65808228929b2a382737ef593eca72ce0e7))
- **backend:** add utility functions for working with FirestoreDb in test enviroment ([f15d24a](https://github.com/callumjgill/pruneurl/commit/f15d24a03c059783c85c8e19f36aa051a052602a))

# [0.8.0](https://github.com/callumjgill/pruneurl/compare/v0.7.0...v0.8.0) (2023-07-16)

### Features

- **backend:** add 'CreateSequenceId' method to EntityTestHelper ([f000b08](https://github.com/callumjgill/pruneurl/commit/f000b085b24abe9a9d810ac55596ead3933e9e3b))
- **backend:** add ISequenceIdFactory interface to application layer ([07d5edc](https://github.com/callumjgill/pruneurl/commit/07d5edce32479755b41c98f224a9437a5d44c2fb))
- **backend:** add SequenceId entity record ([e3891aa](https://github.com/callumjgill/pruneurl/commit/e3891aa33b85fc191692d0fd46e3fd89911da84a))
- **backend:** add SequenceIdFactory implementation to Application layer ([9ffbe7f](https://github.com/callumjgill/pruneurl/commit/9ffbe7f907e78e12d04776a83ccb7993e2b6fe99))

### Tests

- **backend:** add SequenceIdFactoryUnitTests ([6d36b76](https://github.com/callumjgill/pruneurl/commit/6d36b764c98db3a7734eb8f99349d28a5f64d490))
- **backend:** add SequenceIdUnitTests ([b3975b8](https://github.com/callumjgill/pruneurl/commit/b3975b8507bfcf1578b1d613083e796382fae6c1))

# [0.7.0](https://github.com/callumjgill/pruneurl/compare/v0.6.0...v0.7.0) (2023-07-15)

### Features

- **backend:** add ShortUrlProvider implementation of IShortUrlProvider ([0a9a8f8](https://github.com/callumjgill/pruneurl/commit/0a9a8f8b2faf574b75644044952a04042e576a86))

### Tests

- **backend:** add ShortUrlProviderUnitTests ([c10bc16](https://github.com/callumjgill/pruneurl/commit/c10bc16cabe3e26cbaea5ae592686f9d6d80bc8c))
- **backend:** modify ShortUrlFactoryUnitTests to account for refactored ShortUrlFactory code ([eaeef11](https://github.com/callumjgill/pruneurl/commit/eaeef110a7e4b7bd6f430970cc50d1ec397f124d))

### Code Refactoring

- **backend:** extend IShortUrlFactory.Create method with a int? parameter for sequence id ([16db624](https://github.com/callumjgill/pruneurl/commit/16db624788fc3138096d72652ac524b07d7e9a2c))
- **backend:** modify ShortUrlFactory to implement IShortUrlFactory & IShortUrlProvider changes ([88dec1a](https://github.com/callumjgill/pruneurl/commit/88dec1a4b4b60688178769d14624ed405f166b98))
- **backend:** replace string parameter in IShortUrlProvider.GetShortUrl with int parameter ([7911a1a](https://github.com/callumjgill/pruneurl/commit/7911a1ae16af3179522ab4ba83040e84187d3687))

# [0.6.0](https://github.com/callumjgill/pruneurl/compare/v0.5.0...v0.6.0) (2023-07-15)

### Features

- **backend:** add DateTimeProvider implementation of IDateTimeProvider ([2a9e9d3](https://github.com/callumjgill/pruneurl/commit/2a9e9d31916b08028c580a4829ab1df45e3adb95))
- **backend:** add entity project to domain layer ([d3d1640](https://github.com/callumjgill/pruneurl/commit/d3d164099a3329e490861098af87091cbae0b53c))
- **backend:** add IDateTimeProvider interface ([f0b82a7](https://github.com/callumjgill/pruneurl/commit/f0b82a76e57b0b1daf1c2731b595e68df5499884))
- **backend:** add IEntity interface ([ee3b8d4](https://github.com/callumjgill/pruneurl/commit/ee3b8d4ea5d87f2f3933236e16e4f3fac5fe367b))
- **backend:** add IEntityIdProvider interface ([f0ee06f](https://github.com/callumjgill/pruneurl/commit/f0ee06fbca6c195051844f51eb3e6f62a5ef2458))
- **backend:** add implementation project to application layer ([bf68faa](https://github.com/callumjgill/pruneurl/commit/bf68faa89023605cf8eb2c7042fb2e4609e07cdc))
- **backend:** add Interfaces project to application layer ([3ff63b7](https://github.com/callumjgill/pruneurl/commit/3ff63b7c9d2b63995e94e87ef4d31d3210c345be))
- **backend:** add IShortUrlFactory interface ([76261e7](https://github.com/callumjgill/pruneurl/commit/76261e71e64ae8bd1428d02a0253fae726e07319))
- **backend:** add IShortUrlProvider interface ([a4e7152](https://github.com/callumjgill/pruneurl/commit/a4e71524993ba4f430030c94b700b061e704a434))
- **backend:** add ShortUrl entity record ([7128f1f](https://github.com/callumjgill/pruneurl/commit/7128f1f260b9fc8ed471a51d8a7a11d53e7075d7))
- **backend:** add ShortUrlFactory implementation of IShortUrlFactory ([8ed938d](https://github.com/callumjgill/pruneurl/commit/8ed938df95137d84b60fc8b410c646f579daf341))
- **backend:** add TestHelpers project ([f7ecbad](https://github.com/callumjgill/pruneurl/commit/f7ecbad93d8839199582d383b59363f82d8d0bd9))

### Tests

- **backend:** add application implementation test project ([07aac58](https://github.com/callumjgill/pruneurl/commit/07aac58035d5423356cc4537263d128915ed3d4a))
- **backend:** add entity tests project ([9ecb475](https://github.com/callumjgill/pruneurl/commit/9ecb475b631ad6a5ca76b52e594d6c1f50126639))
- **backend:** add EntityTestHelper ([aadcc34](https://github.com/callumjgill/pruneurl/commit/aadcc341a54156ce74cc5f2e42d912476698f929))
- **backend:** add missing Moq dependency to test projects ([e5e6265](https://github.com/callumjgill/pruneurl/commit/e5e6265b478d69bd45713f4331f12985388cc287))
- **backend:** add ShortUrlFactoryUnitTests ([ba46fe3](https://github.com/callumjgill/pruneurl/commit/ba46fe30e58bd72bdf3a99769e8048a9e85884d4))
- **backend:** add ShortUrlUnitTests ([d692358](https://github.com/callumjgill/pruneurl/commit/d692358ef27f23dbd2e02f9411c7356083f81b15))

# [0.5.0](https://github.com/callumjgill/pruneurl/compare/v0.4.0...v0.5.0) (2023-07-14)

### Features

- **backend:** add initial ASP.NET 7 app project & solution ([6ca60e4](https://github.com/callumjgill/pruneurl/commit/6ca60e4ba9a099a19456135bb41bce57669e90d1))
- **backend:** add test templates & solution folders for each architecture layer ([869c729](https://github.com/callumjgill/pruneurl/commit/869c72998cc083f8b1938c6fc546e267cade9668))

# [0.4.0](https://github.com/callumjgill/pruneurl/compare/v0.3.0...v0.4.0) (2023-07-14)

### Features

- **frontend:** add CopyToClipboardTooltip ([1feea50](https://github.com/callumjgill/pruneurl/commit/1feea50d2bf1eb8fe18feb156bb2a4c56a1705de))
- **frontend:** add waitAsync function to time utils ([714155b](https://github.com/callumjgill/pruneurl/commit/714155bce69213eab387d6fd1a4d9f95900ea74d))

### Code Refactoring

- **frontend:** extend CopyToClipboardButton to use tooltip once text copied ([1957ba6](https://github.com/callumjgill/pruneurl/commit/1957ba64ba35fb1ae12d8c25a892165a11b58fc9))
- **frontend:** modify DummyAPI to use waitAsync method instead of its private method ([c7bf91a](https://github.com/callumjgill/pruneurl/commit/c7bf91aff116ae14cecf44f86b290902555016ad))

# [0.3.0](https://github.com/callumjgill/pruneurl/compare/v0.2.0...v0.3.0) (2023-07-13)

### Features

- **frontend:** add 'strings' utils ([5ed4835](https://github.com/callumjgill/pruneurl/commit/5ed4835c89de1dd282eb568ebb1340d08648c2e3))
- **frontend:** add API error enviroment variables for development ([722e373](https://github.com/callumjgill/pruneurl/commit/722e373a63d1708a3c21c059fa365402758f7f9a))
- **frontend:** add API interface and DummyAPI implementation ([a7e2a15](https://github.com/callumjgill/pruneurl/commit/a7e2a15b0dbeed28ba4d8c5307008efb7264337e))
- **frontend:** add getAPI function ([194be4a](https://github.com/callumjgill/pruneurl/commit/194be4a17a110344c02ae0de743770c655bb0f4f))
- **frontend:** add isDevelopment utility ([3530678](https://github.com/callumjgill/pruneurl/commit/3530678c505a67b3e4f944256ff68ce7b85a575a))
- **frontend:** modify PruneUrlForm to submit inputs to API and get returned value ([f191eda](https://github.com/callumjgill/pruneurl/commit/f191eda900d01a72a48d7b7fd317b8685e8a2e71))

### Documentation

- **changelog:** update scopes in changelog ([a4c9264](https://github.com/callumjgill/pruneurl/commit/a4c92643cc136da23b0f7089050d5a439ecf9408))

# [0.2.0](https://github.com/callumjgill/pruneurl/compare/v0.1.1...v0.2.0) (2023-07-09)

### Features

- **frontend:** add responsiveness to app ([51065e0](https://github.com/callumjgill/pruneurl/commit/51065e07c9cb40520bceee7ac0d6c56190a42ba8))
- **frontend:** add useBreakpoint hook ([ee90794](https://github.com/callumjgill/pruneurl/commit/ee9079420e98469d89f9a44ab9cf03671f8a5357))

## [0.1.1](https://github.com/callumjgill/pruneurl/compare/v0.1.0...v0.1.1) (2023-07-09)

### Bug Fixes

- **frontend:** fix spinner size in form buttons ([f190a0f](https://github.com/callumjgill/pruneurl/commit/f190a0fd3bff430ee1cad5932e0450565daa47d2))

### Documentation

- **changelog:** add heading to changelog ([367f7ba](https://github.com/callumjgill/pruneurl/commit/367f7bae584ed968ad6866965f192af5374bc127))

# [0.1.0](https://github.com/callumjgill/pruneurl/compare/v0.0.4...v0.1.0) (2023-07-09)

### Features

- **frontend:** add collapse transition to generated url ([e7bcc17](https://github.com/callumjgill/pruneurl/commit/e7bcc17dbcfd633441cae1763f32719204ce2d39))
- **frontend:** add toast transitions ([22ed83f](https://github.com/callumjgill/pruneurl/commit/22ed83fecc1e85b0bd8ea779c4a94d532f4b0744))

### Code Refactoring

- **frontend:** split form controls into dedicated components ([0450b41](https://github.com/callumjgill/pruneurl/commit/0450b41f73ccdccacf2b64dc8fe6352099d59302))
- **frontend:** consolidate toast types into one component ([cc7cfcf](https://github.com/callumjgill/pruneurl/commit/cc7cfcf0283861d6a10a5c541cff49d5f984f04c))
