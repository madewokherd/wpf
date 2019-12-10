namespace System.IO.Packaging.Internals
{
	partial class SR
	{
		public const string BadPackageFormat = "Format error in package.";
		public const string CannotModifyReadOnlyContainer = "Cannot modify readonly container";
		public const string CannotRetrievePartsOfWriteOnlyContainer = "Cannot retrieve parts of writeonly container";
		public const string ContainerAndPartModeIncompatible = "FileMode/FileAccess for Part.GetStream is not compatible with FileMode/FileAccess used to open the Package.";
		public const string ContentTypeCannotHaveLeadingTrailingLWS = "ContentType string cannot have leading/trailing Linear White Spaces [LWS - RFC 2616].";
		public const string CorePropertiesElementExpected = "Unrecognized root element in Core Properties part.";
		public const string CreateNewNotSupported = "CreateNew not supported";
		public const string DanglingMetadataRelationship = "The target of the Core Properties relationship does not reference an existing part.";
		public const string DefaultTagDoesNotMatchSchema = "Default tag is not valid per the schema. Verify that attributes are correct.";
		public const string DuplicateCorePropertyName = "More than one '{0}' property found.";
		public const string ElementIsNotEmptyElement = "'{0}' element must be empty.";
		public const string EncodingNotSupported = "Encoding format is not supported. Only UTF-8 and UTF-16 are supported.";
		public const string ExpectedRelationshipsElementTag = "Relationships tag expected at root level.";
		public const string ExpectingParameterValuePairs = "';' must be followed by parameter=value pair.";
		public const string ExpectingSemicolon = "Semicolon separator is required between two valid parameter=value pairs.";
		public const string FileFormatException = "Invalid file format.";
		public const string FileFormatExceptionWithFileName = "Invalid file format.";
		public const string GetContentTypeCoreNotImplemented = "PackagePart subclass must implement GetContentTypeCore method if passing a null value for the content type when PackagePart object is constructed.";
		public const string InvalidLinearWhiteSpaceCharacter = "A Linear White Space character is not valid.";
		public const string InvalidParameterValue = "Parameter value must be a valid token or a quoted string as per RFC 2616.";
		public const string InvalidParameterValuePair = "Parameter and value pair is not valid. Expected form is parameter=value.";
		public const string InvalidPartUri = "Part URI is not valid per rules defined in the Open Packaging Conventions specification.";
		public const string InvalidPropertyNameInCorePropertiesPart = "'{0}' property name is not valid in Core Properties part.";
		public const string InvalidRelationshipType = "Relationship Type cannot contain only spaces or be empty.";
		public const string InvalidToken = "A token is not valid. Refer to RFC 2616 for correct grammar of content types.";
		public const string InvalidTypeSubType = "ContentType string is not valid. Expected format is type/subtype.";
		public const string InvalidValueForTheAttribute = "'{0}' attribute value is not valid.";
		public const string InvalidXmlBaseAttributePresent = "Relationships XML elements cannot specify attribute '{0}'.";
		public const string MoreThanOneMetadataRelationships = "Package has more than one Core Properties relationship.";
		public const string NoExternalTargetForMetadataRelationship = "TargetMode for a Core Properties relationship must be 'Internal'.";
		public const string NoStructuredContentInsideProperties = "Core Properties part: core property elements can contain only text data.";
		public const string NotAUniqueRelationshipId = "'{0}' ID conflicts with the ID of an existing relationship for the specified source.";
		public const string NotAValidRelationshipPartUri = "PackageRelationship part URI syntax is not valid.";
		public const string NotAValidXmlIdString = "'{0}' ID is not a valid XSD ID.";
		public const string NullContentTypeProvided = "GetContentTypeCore method cannot return null for the content type stream.";
		public const string NullStreamReturned = "Returned stream for the part is null.";
		public const string ObjectDisposed = "Package object was closed and disposed, so cannot carry out operations on this object or any stream opened on a part of this package.";
		public const string OverrideTagDoesNotMatchSchema = "Override tag is not valid per the schema. Verify that attributes are correct.";
		public const string PackagePartDeleted = "Part was deleted.";
		public const string PackagePartRelationshipDoesNotExist = "PackageRelationship with specified ID does not exist for the source part.";
		public const string PackageRelationshipDoesNotExist = "PackageRelationship with specified ID does not exist at the Package level.";
		public const string ParentContainerClosed = "Cannot access part because parent package was closed.";
		public const string PartAlreadyExists = "Cannot add part for the specified URI because it is already in the package.";
		public const string PartDoesNotExist = "Specified part does not exist in the package.";
		public const string PartNamePrefixExists = "Cannot add part to the package. Part names cannot be derived from another part name by appending segments to it.";
		public const string PartUriCannotHaveAFragment = "Part URI cannot contain a Fragment component.";
		public const string PartUriIsEmpty = "Part URI is empty.";
		public const string PartUriShouldNotEndWithForwardSlash = "Part URI cannot end with a forward slash.";
		public const string PartUriShouldNotStartWithTwoForwardSlashes = "Part URI cannot start with two forward slashes.";
		public const string PartUriShouldStartWithForwardSlash = "Part URI must start with a forward slash.";
		public const string PropertyStartTagExpected = "Core Properties part: A property start-tag was expected.";
		public const string PropertyWrongNumbOfAttribsDefinedOn = "Unexpected number of attributes is found on '{0}'.";
		public const string RelationshipPartIncorrectContentType = "Incorrect content type for PackageRelationship part.";
		public const string RelationshipPartsCannotHaveRelationships = "PackageRelationship parts cannot have relationships to other parts.";
		public const string RelationshipPartUriExpected = "PackageRelationship part URI is expected.";
		public const string RelationshipPartUriNotExpected = "PackageRelationship part URI is not expected.";
		public const string RelationshipsTagHasExtraAttributes = "Relationships tag has extra attributes.";
		public const string RelationshipTagDoesntMatchSchema = "Relationship tag contains incorrect attribute.";
		public const string RelationshipTargetMustBeRelative = "PackageRelationship target must be relative URI if TargetMode is Internal.";
		public const string RelationshipToRelationshipIllegal = "PackageRelationship cannot target another PackageRelationship.";
		public const string RequiredAttributeEmpty = "'{0}' tag requires a nonempty '{1}' attribute.";
		public const string RequiredAttributeMissing = "'{0}' tag requires attribute '{1}'.";
		public const string RequiredRelationshipAttributeMissing = "Relationship tag requires attribute '{0}'.";
		public const string StreamObjectDisposed = "Cannot access Stream object because it was closed or disposed.";
		public const string TruncateNotSupported = "Truncate not supported";
		public const string TypesElementExpected = "Required Types tag not found.";
		public const string TypesTagHasExtraAttributes = "Types tag has attributes not valid per the schema.";
		public const string TypesXmlDoesNotMatchSchema = "Content Types XML does not match schema.";
		public const string UnknownDCDateTimeXsiType = "Unknown xsi:type for DateTime on '{0}'.";
		public const string UnknownNamespaceInCorePropertiesPart = "Unrecognized namespace in Core Properties part.";
		public const string UnknownTagEncountered = "Unrecognized tag found in Relationships XML.";
		public const string UnsupportedCombinationOfModeAccess = "Cannot get stream with FileMode.Create, FileMode.CreateNew, FileMode.Truncate, FileMode.Append when access is FileAccess.Read.";
		public const string URIShouldNotBeAbsolute = "Cannot be an absolute URI.";
		public const string WrongContentTypeForPropertyPart = "The Core Properties relationship references a part that has an incorrect content type.";
		public const string XCRChoiceAfterFallback = "Choice cannot follow a Fallback.";
		public const string XCRChoiceNotFound = "AlternateContent must contain one or more Choice elements.";
		public const string XCRChoiceOnlyInAC = "Choice is valid only in AlternateContent.";
		public const string XCRCompatCycle = "There is a cycle of XML compatibility definitions, such that namespace '{0}' overrides itself. This could be due to inconsistent XmlnsCompatibilityAttributes in different assemblies. Please change the definitions to eliminate this cycle.";
		public const string XCRDuplicatePreserve = "Duplicate Preserve declaration for element {1} in namespace '{0}'.";
		public const string XCRDuplicateProcessContent = "Duplicate ProcessContent declaration for element '{1}' in namespace '{0}'.";
		public const string XCRDuplicateWildcardPreserve = "Duplicate wildcard Preserve declaration for namespace '{0}'.";
		public const string XCRDuplicateWildcardProcessContent = "Duplicate wildcard ProcessContent declaration for namespace '{0}'.";
		public const string XCRFallbackOnlyInAC = "Fallback is valid only in AlternateContent.";
		public const string XCRInvalidACChild = "'{0}' element is not a valid child of AlternateContent. Only Choice and Fallback elements are valid children of an AlternateContent element.";
		public const string XCRInvalidAttribInElement = "'{0}' attribute is not valid for '{1}' element.";
		public const string XCRInvalidFormat = "'{0}' format is not valid.";
		public const string XCRInvalidPreserve = "Cannot have both a specific and a wildcard Preserve declaration for namespace '{0}'.";
		public const string XCRInvalidProcessContent = "Cannot have both a specific and a wildcard ProcessContent declaration for namespace '{0}'.";
		public const string XCRInvalidRequiresAttribute = "Requires attribute must contain a valid namespace prefix.";
		public const string XCRInvalidXMLName = "'{0}' attribute value is not a valid XML name.";
		public const string XCRMultipleFallbackFound = "AlternateContent must contain only one Fallback element.";
		public const string XCRMustUnderstandFailed = "MustUnderstand condition failed on namespace '{0}'";
		public const string XCRNSPreserveNotIgnorable = "'{0}' namespace cannot preserve items; it must be declared Ignorable first.";
		public const string XCRNSProcessContentNotIgnorable = "'{0}' namespace cannot process content; it must be declared Ignorable first.";
		public const string XCRRequiresAttribNotFound = "Choice must contain Requires attribute.";
		public const string XCRUndefinedPrefix = "'{0}' prefix is not defined.";
		public const string XCRUnknownCompatAttrib = "Unrecognized compatibility attribute '{0}'.";
		public const string XCRUnknownCompatElement = "Unrecognized Compatibility element '{0}'.";
		public const string XsdDateTimeExpected = "Core Properties part: Text data of XSD type 'DateTime' was expected.";
		public const string CreateNewOnNonEmptyStream = "CreateNew is not a valid FileMode for a non-empty stream.";
		public const string ZipZeroSizeFileIsNotValidArchive = "Archive file cannot be size 0.";
		public const string FragmentMustStartWithHash = "The 'fragment' parameter must start with a number sign.";
		public const string UriShouldBeAbsolute = "Must have absolute URI.";
		public const string UriShouldBePackScheme = "URI must contain pack:// scheme.";
		public const string InnerPackageUriHasFragment = "Package URI obtained from the pack URI cannot contain a Fragment.";
		public const string ReadNotSupported = "Stream does not support reading.";
		public const string OffsetNegative = "Offset must be greater than or equal to zero.";
		public const string ReadCountNegative = "Count of bytes to read cannot be negative.";
		public const string ReadBufferTooSmall = "Buffer too small to hold results of Read.";
		public const string WriteNotSupported = "Stream does not support writing.";
		public const string WriteCountNegative = "Count of bytes to write cannot be negative.";
		public const string WriteBufferTooSmall = "Buffer of bytes to be written is too small.";

		public static string Format(string id, params object[] args)
		{
			return string.Format(id, args);
		}
	}
}
