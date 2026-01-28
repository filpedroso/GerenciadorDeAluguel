# .NET 8 Upgrade Plan - GerenciadorDeAluguel

## Table of Contents

- [Executive Summary](#executive-summary)
- [Migration Strategy](#migration-strategy)
- [Detailed Dependency Analysis](#detailed-dependency-analysis)
- [Project-by-Project Plans](#project-by-project-plans)
  - [GerenciadorDeAluguel.csproj](#gerenciadordealuguelcsproj)
- [Package Update Reference](#package-update-reference)
- [Breaking Changes Catalog](#breaking-changes-catalog)
- [Risk Management](#risk-management)
- [Testing & Validation Strategy](#testing--validation-strategy)
- [Complexity & Effort Assessment](#complexity--effort-assessment)
- [Source Control Strategy](#source-control-strategy)
- [Success Criteria](#success-criteria)

---

## Executive Summary

### Scenario Overview

| Attribute | Value |
|-----------|-------|
| **Solution** | GerenciadorDeAluguel.slnx |
| **Current Framework** | .NET 6.0 |
| **Target Framework** | .NET 8.0 (LTS) |
| **Total Projects** | 1 |
| **Total Lines of Code** | 47 |
| **Estimated LOC Impact** | Minimal (~0%) |

### Selected Strategy

**All-At-Once Strategy** - Single project upgraded in one atomic operation.

**Rationale**:
- Only 1 project in solution (simple structure)
- No project dependencies to manage
- Small codebase (47 LOC)
- No security vulnerabilities detected
- Only 1 package requires attention (deprecated xunit)
- All APIs are compatible with .NET 8.0

### Complexity Classification

| Metric | Value | Assessment |
|--------|-------|------------|
| Project Count | 1 | ? Simple |
| Dependency Depth | 0 | ? None |
| High-Risk Items | 0 | ? None |
| Security Vulnerabilities | 0 | ? None |
| API Compatibility | 100% | ? Fully compatible |

**Classification**: **Simple** - Fast batch upgrade with minimal iterations.

### Critical Issues

- ?? **Deprecated Package**: `xunit 2.9.3` is marked as deprecated - package is still functional but flagged for attention

---

## Migration Strategy

### Approach: All-At-Once (Atomic Upgrade)

All framework and package updates will be performed simultaneously in a single coordinated operation with no intermediate states.

### Justification

| Criteria | Assessment | Supports All-At-Once |
|----------|------------|----------------------|
| Solution Size | 1 project | ? Yes |
| Dependency Complexity | None | ? Yes |
| Package Compatibility | 75% compatible, 25% deprecated | ? Yes |
| API Compatibility | 100% compatible | ? Yes |
| Risk Level | Low | ? Yes |

### Execution Approach

1. **Single Atomic Operation**: Update TargetFramework and all package references in one pass
2. **Immediate Validation**: Build and test after all changes applied
3. **No Intermediate States**: Solution either fully upgraded or unchanged

### Implementation Timeline

| Phase | Description | Deliverable |
|-------|-------------|-------------|
| **Phase 0** | Preparation - Verify .NET 8 SDK installed | SDK ready |
| **Phase 1** | Atomic Upgrade - Update project and packages | Solution builds with 0 errors |
| **Phase 2** | Test Validation - Execute all tests | All tests pass |

---

## Detailed Dependency Analysis

### Dependency Graph Summary

```
GerenciadorDeAluguel.csproj (net6.0 ? net8.0)
??? No project dependencies
```

### Project Groupings

| Phase | Projects | Rationale |
|-------|----------|-----------|
| **Phase 1: Atomic Upgrade** | GerenciadorDeAluguel.csproj | Single project, no dependencies |

### Critical Path

Not applicable - single project solution with no inter-project dependencies.

### Circular Dependencies

None detected.

---

## Project-by-Project Plans

### GerenciadorDeAluguel.csproj

#### Current State

| Attribute | Value |
|-----------|-------|
| **Target Framework** | net6.0 |
| **Project Type** | DotNetCoreApp |
| **SDK-style** | Yes |
| **Lines of Code** | 47 |
| **Files** | 3 |
| **NuGet Packages** | 4 |
| **Project Dependencies** | 0 |
| **Dependants** | 0 |
| **Risk Level** | ?? Low |

#### Target State

| Attribute | Value |
|-----------|-------|
| **Target Framework** | net8.0 |
| **Package Updates** | 0 (xunit deprecated but functional) |

#### Migration Steps

1. **Prerequisites**
   - Verify .NET 8 SDK is installed on the machine
   
2. **Framework Update**
   - Update `TargetFramework` from `net6.0` to `net8.0` in project file

3. **Package Updates**
   - No package version updates required
   - `xunit 2.9.3` is deprecated but remains functional for .NET 8.0
   - All other packages are compatible as-is

4. **Code Modifications**
   - No code changes expected (100% API compatibility)

5. **Build Validation**
   - Restore dependencies
   - Build solution
   - Verify 0 errors and 0 warnings

6. **Test Execution**
   - Run all xUnit tests
   - Verify all tests pass

#### Validation Checklist

- [ ] TargetFramework updated to net8.0
- [ ] `dotnet restore` succeeds
- [ ] `dotnet build` succeeds with 0 errors
- [ ] `dotnet build` succeeds with 0 warnings
- [ ] All unit tests pass

---

## Package Update Reference

### Package Compatibility Summary

| Package | Current Version | Target Version | Status | Notes |
|---------|-----------------|----------------|--------|-------|
| Microsoft.NET.Test.Sdk | 18.0.1 | 18.0.1 | ? Compatible | No update required |
| Newtonsoft.Json | 13.0.4 | 13.0.4 | ? Compatible | No update required |
| xunit | 2.9.3 | 2.9.3 | ?? Deprecated | Package still functional, consider future migration |
| xunit.runner.visualstudio | 2.5.6 | 2.5.6 | ? Compatible | No update required |

### Package Update Actions

**No package version updates required for .NET 8.0 compatibility.**

All packages are either:
- Already compatible with .NET 8.0
- Deprecated but still functional (xunit)

### Future Consideration

The `xunit` package is marked as deprecated. While it continues to work, consider:
- Monitoring for official migration guidance
- Evaluating alternative test frameworks if xunit becomes unsupported

---

## Breaking Changes Catalog

### .NET 6 to .NET 8 Breaking Changes

**Assessment Result**: No breaking changes detected for this codebase.

| Category | Count | Impact |
|----------|-------|--------|
| Binary Incompatible APIs | 0 | None |
| Source Incompatible APIs | 0 | None |
| Behavioral Changes | 0 | None |
| **Total APIs Analyzed** | **32** | **All Compatible** |

### Expected Code Changes

None required. All 32 analyzed APIs are fully compatible with .NET 8.0.

### Areas to Monitor

While no breaking changes were detected, the following areas may have subtle behavioral differences:
- Improved performance characteristics in .NET 8
- Updated default configurations
- Enhanced security defaults

---

## Risk Management

### Risk Assessment

| Project | Risk Level | Description | Mitigation |
|---------|------------|-------------|------------|
| GerenciadorDeAluguel.csproj | ?? Low | Simple project, full API compatibility | Standard upgrade process |

### Security Vulnerabilities

**None detected.** All NuGet packages are free of known security vulnerabilities.

### Potential Issues

| Issue | Probability | Impact | Mitigation |
|-------|-------------|--------|------------|
| Deprecated xunit package | Low | Low | Package still functional; monitor for updates |
| Build environment missing .NET 8 SDK | Medium | High | Verify SDK installation before starting |

### Contingency Plan

If upgrade fails:
1. Revert TargetFramework change in project file
2. Run `dotnet restore` to restore original packages
3. Verify solution builds and tests pass
4. Investigate specific failure before retrying

---

## Testing & Validation Strategy

### Test Projects Identified

| Project | Test Framework | Test Count |
|---------|----------------|------------|
| GerenciadorDeAluguel.csproj | xUnit | 1+ |

### Validation Phases

#### Phase 1: Build Validation
- Restore all NuGet packages
- Build solution in Release configuration
- Verify 0 errors
- Verify 0 warnings (or document accepted warnings)

#### Phase 2: Test Execution
- Execute all xUnit tests via `dotnet test`
- Verify all tests pass
- Document any test failures with root cause

### Success Metrics

| Metric | Target |
|--------|--------|
| Build Errors | 0 |
| Build Warnings | 0 |
| Test Pass Rate | 100% |

---

## Complexity & Effort Assessment

### Project Complexity

| Project | Complexity | Dependencies | Package Issues | API Issues | Risk |
|---------|------------|--------------|----------------|------------|------|
| GerenciadorDeAluguel.csproj | ?? Low | 0 | 1 (deprecated) | 0 | ?? Low |

### Phase Complexity

| Phase | Description | Complexity | Notes |
|-------|-------------|------------|-------|
| Phase 0 | SDK Verification | ?? Low | One-time check |
| Phase 1 | Atomic Upgrade | ?? Low | Single project, no breaking changes |
| Phase 2 | Test Validation | ?? Low | Standard test execution |

### Resource Requirements

| Requirement | Level |
|-------------|-------|
| .NET Knowledge | Basic |
| Project Familiarity | Basic |
| Parallel Capacity | N/A (single project) |

---

## Source Control Strategy

### Repository Status

**No Git repository detected** for this solution.

### Recommendations

If version control is added in the future:

1. **Single Commit Approach** (Recommended for All-At-Once)
   - All upgrade changes in one atomic commit
   - Commit message: `chore: upgrade to .NET 8.0`
   
2. **Backup Strategy**
   - Create a backup of the solution folder before starting
   - Keep backup until upgrade is validated

### Current Approach

Without version control:
- Create manual backup before upgrade
- Validate upgrade thoroughly before deleting backup

---

## Success Criteria

### Technical Criteria

- [ ] All projects target `net8.0`
- [ ] All package references resolved
- [ ] Solution builds with 0 errors
- [ ] Solution builds with 0 warnings
- [ ] All unit tests pass
- [ ] No security vulnerabilities

### Quality Criteria

- [ ] Code quality maintained (no regressions)
- [ ] Test coverage maintained
- [ ] Documentation updated (if applicable)

### Process Criteria

- [ ] All-At-Once strategy followed
- [ ] Validation checklist completed
- [ ] Backup created before upgrade (if no source control)

### Definition of Done

The .NET 8 upgrade is complete when:
1. `GerenciadorDeAluguel.csproj` targets `net8.0`
2. `dotnet build` succeeds with 0 errors and 0 warnings
3. `dotnet test` executes all tests with 100% pass rate
4. No security vulnerabilities exist in dependencies
